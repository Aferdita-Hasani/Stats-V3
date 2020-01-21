using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stats_V3.Models
{
    public class FullFormularViewModel
    {
        public Formulari formulari { get; set; }
        public List<FormulariDetajet> formulariDetajet { get; set; }
        public List<SuksesiLendor> suksesiLendor { get; set; }


        public SelectList lenda { get; set; }
        public SelectList suksesi { get; set; }

        public string suksesiLendorJson {get; set; }




}
}