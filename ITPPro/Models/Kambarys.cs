using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITPPro.Models.Enums;

namespace ITPPro.Models
{
    public class Kambarys
    {
        public int id { get; set; }
        public int vietu_sk { get; set; }
        public string numeris { get; set; }
        public float kaina { get; set;}
        public string aprasymas { get; set; }
        public virtual Kambario_Tipai_Enum tipas { get; set; }
        public int fk_Viesbutisid { get; set; }
    }
}