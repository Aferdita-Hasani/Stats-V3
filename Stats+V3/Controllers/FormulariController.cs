using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            //var formularis = db.Formularis.Include(f => f.Gjenerata).Include(f => f.Gjysmevjetori).Include(f => f.Shkolla);
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
            Formulari formulari = DAL.DAL_Formulari.Read(id);
            if (formulari == null)
            {
                return HttpNotFound();
            }
            return View(formulari);
        }

        // GET: Formulari/Create
        public ActionResult Create()
        {
            var model = new FullFormularViewModel();
            model.formulari = new Formulari();
            model.formulari.ListaGjenerata= new SelectList(DAL.DAL_Gjenerata.ListGjenerata(), "Id", "FullInfo");
            model.formulari.ListaGjysemvjetori = new SelectList(DAL.DAL_Gjysmevjetori.List(), "Id", "Gjysmevjetori1");
           model.lenda= new SelectList(DAL.DAL_Lenda.List(), "Id", "Emertimi");
            model.suksesi = new SelectList(DAL.DAL_Suksesi.List(), "Id", "Emertimi");
            return View(model);
        }

        // POST: Formulari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Formulari formulari)
        {
            if (ModelState.IsValid)
            {
                DAL.DAL_Formulari.Create(formulari);
                return RedirectToAction("Index");
            }

            var model = new FullFormularViewModel();
            model.formulari = new Formulari();
            model.formulari.ListaGjenerata = new SelectList(DAL.DAL_Gjenerata.ListGjenerata(), "Id", "FullInfo");
            model.formulari.ListaGjysemvjetori = new SelectList(DAL.DAL_Gjysmevjetori.List(), "Id", "Gjysmevjetori1");
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
    }
}
