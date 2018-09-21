using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPPro.Models
{
    public class Viesbutis
    {
        public int id { get; set; }
        public string pavadinimas { get; set; }
        public string viesbuciu_tinklas { get; set; }
        public int zvaigzduciu_sk { get; set; }
        public string miestas { get; set; }
        public string adresas { get; set; }
        public string aprasymas { get; set; }
        public int fk_savininkas { get; set; }

    }
}