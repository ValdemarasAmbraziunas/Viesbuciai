using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITPPro.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ITPPro.Models
{
    public class Darbuotojas
    {
        [Key]
        public int darbuojo_kodas { get; set; }
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public string adresas { get; set; }
        public string telefonas { get; set; }
        public string lytis { get; set; }
        public DateTime darbo_pradzios_laikas { get; set; }
        public string slaptazodis { get; set; }

        public virtual Darbuotoju_Tipai_Enum darbuotojo_tipas { get; set; }
        public int? fk_Viesbutisid { get; set; }

        public string el_pastas { get; set; }
    }
}