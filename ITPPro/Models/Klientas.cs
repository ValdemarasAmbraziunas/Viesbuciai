using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.Models
{
    public class Klientas
    {
        [Key]
        public int kliento_kodas { get; set; }
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public string el_pastas { get; set; }
        public string lytis { get; set; }
        public string telefonas { get; set; }
        public string adresas { get; set; }
        public DateTime sukurimo_data { get; set; }
        public string slaptazodis { get; set; }
    }
}