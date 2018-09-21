using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITPPro.Models.Enums;

namespace ITPPro.Models
{
    public class Rezervacija
    {
        public int id { get; set; }
        public DateTime rezervacijos_pradzia { get; set; }
        public DateTime rezervacijos_pabaiga { get; set; }
        public DateTime rezervacijos_atlikimo_data { get; set; }
        public virtual Rezervacijos_Busena_Enum busena { get; set; }
        public int fk_Klientaskliento_kodas { get; set; }
    }
}