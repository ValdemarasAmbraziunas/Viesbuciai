using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITPPro.Models.Enums;

namespace ITPPro.Models
{
    public class Mokejimas
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public string atsiskaitymo_budas { get; set; }
        public string suma { get; set; }
        public DateTime apmokejimo_data { get; set; }
        public virtual Mokejimo_Tipai_Enum tipas { get; set; }
        public virtual Mokejimo_Busenos_Enum busena { get; set; }
        public int fk_Rezervacijaid { get; set; }
        public int fk_Klientaskliento_kodas { get; set; }
    }
}