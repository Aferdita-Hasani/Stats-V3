using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stats_V3.Controllers
{
    public class FormulariDetajetController : Controller
    {
        // GET: FormulariDetajet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            return View();
        }

        public ActionResult Create(FormulariDetajet formulariDetajet)
        {
            return RedirectToAction("Detajet", "Formulari", formulariDetajet.FormulariId);
        }
    }
}