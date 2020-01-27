using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stats_V3.Models
{
    public class FullFormularViewModel:Formulari
    {
 
        public List<FormulariDetajet> formulariDetajet { get; set; }
        public List<SuksesiLendor> suksesiLendor { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Nuk mund te vendosni numra negativ")]
        public Nullable<int> oretEPlanifikuara { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Nuk mund te vendosni numra negativ")]

        public Nullable<int> oretEMbajtura { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Nuk mund te vendosni numra negativ")]

        public Nullable<int> nrFemra { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Nuk mund te vendosni numra negativ")]

        public Nullable<int> nrMeshkuj { get; set; }
        public int lendaId { get; set; }
        public int suksesiId { get; set; }

        public SelectList lenda { get; set; }
        public SelectList suksesi { get; set; }

        public string formulariDetajetJson { get; set; }
        public string suksesiLendorJson { get; set; }




    }
}