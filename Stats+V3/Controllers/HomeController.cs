using Stats_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stats_V3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        #region VitiShkollor
        public ActionResult ShowVitiShkollor()
        {

            //var userShkolla = new UserShkolla();
            //userShkolla.ListaUser = new SelectList(, "Id", "FullInfo");


            return View(DAL.DAL_VitiShkollor.List());
        }

        public ActionResult CreateVitiShkollore()
        {
            return View();
        }     


        // POST: VitiShkollore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVitiShkollore(VitiShkollore vitiShkollore)
        {
            if (ModelState.IsValid)
            {
                DAL.DAL_VitiShkollor.Create(vitiShkollore);
                return RedirectToAction("ShowVitiShkollor");
            }

            return View(vitiShkollore);
        }
        #endregion

        #region Gjysmevjetori
        public ActionResult ShowGjysmevjetori()
        {


            return View(DAL.DAL_Gjysmevjetori.List());
        }

        public ActionResult CreateGjysmevjetori()
        {
            var gjysmevjetori = new Gjysmevjetori();
            gjysmevjetori.ListaVitiShkollor = new SelectList(DAL.DAL_VitiShkollor.List(), "Id", "VitiShkollor");
            return View(gjysmevjetori);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGjysmevjetori(Gjysmevjetori gjysmevjetori)
        {
            if (ModelState.IsValid)
            {
                DAL.DAL_Gjysmevjetori.Create(gjysmevjetori);
                return RedirectToAction("ShowGjysmevjetori");
            }

            return View(gjysmevjetori);
        }
        #endregion

        #region Lenda
        public ActionResult ShowLenda()
        {


            return View(DAL.DAL_Lenda.List());
        }

        public ActionResult CreateLenda()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLEnda(Lenda lenda)
        {
            if (ModelState.IsValid)
            {
              int lendaId=   DAL.DAL_Lenda.Create(lenda);
                var klasatCheckec = lenda.KlasatJson.Split(',');
                for (int i=0;i< klasatCheckec.Length-1;i++)
                {
                    DAL.DAL_Lenda.CreateLendaKlasa(lendaId, int.Parse(klasatCheckec[i]));
                }
                return RedirectToAction("ShowLenda");
            }

            return View(lenda);
        }
        #endregion

        #region Shkolla
        public ActionResult ShowShkolla()
        {


            return View(DAL.DAL_Shkolla.List());
        }

        public ActionResult CreateShkolla()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShkolla(Shkolla shkolla)
        {
            if (ModelState.IsValid)
            {
                DAL.DAL_Shkolla.Create(shkolla);
                return RedirectToAction("ShowShkolla");
            }

            return View(shkolla);
        }
        #endregion

        #region Gjenerata
        public ActionResult ShowGjenerata()
        {


            return View(DAL.DAL_Gjenerata.List());
        }

        public ActionResult CreateGjenerata()
        {
            var gjenerata = new Gjenerata();
            gjenerata.ListaVitiShkollor = new SelectList(DAL.DAL_VitiShkollor.List(), "Id", "VitiShkollor");

            return View(gjenerata);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGjenerata(Gjenerata gjenerata)
        {
            if (ModelState.IsValid)
            {
                DAL.DAL_Gjenerata.Create(gjenerata);
                return RedirectToAction("ShowGjenerata");
            }

            return View(gjenerata);
        }
        #endregion

     

    }

}