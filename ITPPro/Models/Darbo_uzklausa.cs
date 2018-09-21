using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPPro.Models
{
    public class Darbo_uzklausa
    {
        public int id { get; set; }
        public string pareigos { get; set; }
        public string slaptazodis { get; set; }
        public int fk_Darbuotojasdarbuojo_kodas { get; set; }
        public int fk_Klientaskliento_kodas { get; set; }
    }
}