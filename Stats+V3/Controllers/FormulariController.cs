using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Stats_V3;
using Stats_V3.Models;

namespace Stats_V3.Controllers
{
    public class FormulariController : Controller
    {
        private StatsV4Entities db = new StatsV4Entities();
        
        // GET: Formulari
        public ActionResult Index()
        {
            var formulari = DAL.DAL_Formulari.ListFormular();
            
            return View(formulari.ToList());
        }
      

        // GET: Formulari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullFormularViewModel formulari = new FullFormularViewModel();
            Formulari f = DAL.DAL_Formulari.Read(id);
            formulari.MungesaMeArsye = f.MungesaMeArsye;
            formulari.MungesaPaArsye = f.MungesaPaArsye;
            
            formulari.suksesiLendor= DAL.DAL_SuksesiLendor.List(id);
            if (formulari == null)
            {
                return HttpNotFound();
            }
            return View(formulari);
        }

        // GET: Formulari/Create
        public ActionResult Create()
        {
            FullFormularViewModel model = new FullFormularViewModel();
           
            model.ListaGjenerata= new SelectList(DAL.DAL_Gjenerata.List(), "Id", "FullInfo");
            model.ListaGjysemvjetori = new SelectList(DAL.DAL_Gjysmevjetori.List(), "Id", "FullInfo");
           model.lenda= new SelectList(DAL.DAL_Lenda.List(), "Id", "Emertimi");
            model.suksesi = new SelectList(DAL.DAL_Suksesi.List(), "Id", "Emertimi");
        
            return View(model);
        }

        // POST: Formulari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FullFormularViewModel formulari)
        {
            if (ModelState.IsValid)
            {
               


                int id =DAL.DAL_Formulari.Create(formulari);
              formulari.formulariDetajet=  FormulariDetajetFromJson(formulari.formulariDetajetJson, id, formulari.suksesiLendorJson);

                foreach (var item in formulari.formulariDetajet)
                {
                    DAL.DAL_FormulariDetaje.Create(item);
                }
                return RedirectToAction("Index");
            }

            var model = new FullFormularViewModel();
           
            model.ListaGjenerata = new SelectList(DAL.DAL_Gjenerata.List(), "Id", "FullInfo");
            model.ListaGjysemvjetori = new SelectList(DAL.DAL_Gjysmevjetori.List(), "Id", "Gjysmevjetori1");
            model.lenda = new SelectList(DAL.DAL_Lenda.List(), "Id", "Emertimi");
            model.suksesi = new SelectList(DAL.DAL_Suksesi.List(), "Id", "Emertimi");
            return View(model);
        }

        // GET: Formulari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulari formulari = db.Formularis.Find(id);
            if (formulari == null)
            {
                return HttpNotFound();
            }
            ViewBag.GjenerataId = new SelectList(db.Gjeneratas, "Id", "Id", formulari.GjenerataId);
            ViewBag.GjysmevjetoriId = new SelectList(db.Gjysmevjetoris, "Id", "Id", formulari.GjysmevjetoriId);
            ViewBag.ShkollaId = new SelectList(db.Shkollas, "Id", "Emertimi", formulari.ShkollaId);
            return View(formulari);
        }

        // POST: Formulari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GjenerataId,GjysmevjetoriId,ShkollaId,MungesaMeArsye,MungesaPaArsye")] Formulari formulari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formulari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GjenerataId = new SelectList(db.Gjeneratas, "Id", "Id", formulari.GjenerataId);
            ViewBag.GjysmevjetoriId = new SelectList(db.Gjysmevjetoris, "Id", "Id", formulari.GjysmevjetoriId);
            ViewBag.ShkollaId = new SelectList(db.Shkollas, "Id", "Emertimi", formulari.ShkollaId);
            return View(formulari);
        }

        // GET: Formulari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulari formulari = db.Formularis.Find(id);
            if (formulari == null)
            {
                return HttpNotFound();
            }
            return View(formulari);
        }

        // POST: Formulari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Formulari formulari = db.Formularis.Find(id);
            db.Formularis.Remove(formulari);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<SuksesiLendor> SuksesiLendorFromJson(string json)
        {
            var rows = json.Split(';');
            List<SuksesiLendor> suksesiLendor = new List<SuksesiLendor>(); 
          
            if (rows !=null)
            {
                for (int i = 0; i < rows.Length-1; i++)
                {
                    var recordSplitd = rows[i].Split(',');
                    SuksesiLendor temp = new SuksesiLendor() { 
                        
                        LendaId=int.Parse(recordSplitd[0]),
                        SuksesiId= int.Parse(recordSplitd[1]),
                        NrNxenesveFemra= int.Parse(recordSplitd[2]),
                        NrNxenesveMeshkuj= int.Parse(recordSplitd[3])
                    };
                    suksesiLendor.Add(temp);
                }
            }
            return suksesiLendor;
        }
        public List<FormulariDetajet> FormulariDetajetFromJson(string detajetJson, int formulariId,string suksesiJson)
        {
            var rows = detajetJson.Split(';');
            List<FormulariDetajet> detajet = new List<FormulariDetajet>();
            if (rows != null)
            {
                for (int i = 0; i < rows.Length - 1; i++)
                {
                    var recordSplitd = rows[i].Split(',');
                    FormulariDetajet temp = new FormulariDetajet()
                    {
                        FormulariId= formulariId,
                        OretEMbajtura = int.Parse(recordSplitd[2]),
                        OretEPlanifikuara = int.Parse(recordSplitd[1]),
                     
                    };
                    foreach(SuksesiLendor suksesi in SuksesiLendorFromJson(suksesiJson))
                    {
                        if(int.Parse(recordSplitd[0]) == suksesi.LendaId)
                        {
                           
                            temp.SuksesiLendors.Add(suksesi);
                        }
                    }
                    detajet.Add(temp);
                }
            }
            return detajet;
        }
    }
   
}
