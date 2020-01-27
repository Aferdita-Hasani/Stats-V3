using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stats_V3.Models
{
    public class RaportiIGjeneratesViewModel
    {
        public int Klasa { get; set; }
        public int Paralelja { get; set; }
        public int NrNxeneseFemra { get; set; }
        public int NrNxenesM { get; set; }
        public string SuksesiEmertimi { get; set; }
        public int SuksesiNrF { get; set; }
        public int SuksesiNrM { get; set; }

        public int MungesaPaArsye { get; set; }
        public int MungesaMeArsye { get; set; }
    }
    public class RaportiIGjenerates
    {
        public int Klasa { get; set; }
        
        public int NrParalele { get; set; }
        public int NrNxeneseF { get; set; }
        public int NrNxeneseM { get; set; }
        public SelectList GjysmevjetoriList { get; set; }
        public SelectList KlasaList { get; set; }

        public List<SuksesiNumriNxenesa> Suksesi { get; set; }
        public int NrMungesaPaArsye { get; set; }
        public int NrMungesMeArsye { get; set; }
        public int NrMungesaTotal { get; set; }
    }
    public class SuksesiNumriNxenesa
    {
        public string Suksesi { get; set; }
        public int NrFemra { get; set; }
        public int NrMeshkuj { get; set; }
    }

}