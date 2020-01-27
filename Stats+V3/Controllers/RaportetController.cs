using Stats_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stats_V3.Controllers
{
    public class RaportetController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RaportiGjenerates()

        {          

            return View(RaportiIGjenerates(DAL.DAL_Raporti.List(1, 1)));
        }
        public List<RaportiIGjenerates> RaportiIGjenerates(List<RaportiIGjeneratesViewModel> models)
        {
            var distinctGjenerat = models.Select(x => x.Klasa).Distinct();
            var suksesi = models.Select(m => m.SuksesiEmertimi).Distinct();
            List<RaportiIGjenerates> raportiIGjenerates = new List<RaportiIGjenerates>();
            foreach (var item in distinctGjenerat)
            {
                raportiIGjenerates.Add(new Models.RaportiIGjenerates()
                {
                    Klasa = item
                });
            }

            foreach (var item in raportiIGjenerates)
            {
                var records = models.Where(x => x.Klasa == item.Klasa).ToList();
                var paralelet = models.Select(x => x.Paralelja).Distinct();
                foreach (var paralele in paralelet)
                {
                    var pRecords = records.Where(x => x.Paralelja == paralele).FirstOrDefault();
                    if (pRecords!=null)
                    {
                        item.NrNxeneseF += pRecords.NrNxeneseFemra;
                        item.NrNxeneseM += pRecords.NrNxenesM;
                        item.NrMungesaPaArsye += pRecords.MungesaPaArsye;
                        item.NrMungesMeArsye += pRecords.MungesaMeArsye;
                    }
                    
                }
                item.NrParalele = paralelet.Count();

                item.NrMungesaTotal = item.NrMungesMeArsye + item.NrMungesaPaArsye;
                item.Suksesi = new List<SuksesiNumriNxenesa>();
                foreach (var sukses in suksesi)
                {
                    var suksesData = records.Where(m => m.SuksesiEmertimi == sukses);
                    var tempObj = new SuksesiNumriNxenesa();
                    tempObj.Suksesi = sukses;
                    foreach (var suksesRecord in suksesData)
                    {
                        tempObj.NrFemra += suksesRecord.SuksesiNrF;
                        tempObj.NrMeshkuj += suksesRecord.SuksesiNrM;
                    }
                    item.Suksesi.Add(tempObj);
                }

            }
            return raportiIGjenerates;
        }
    }
}