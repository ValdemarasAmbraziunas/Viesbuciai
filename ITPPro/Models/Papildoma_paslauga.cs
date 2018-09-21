using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPPro.Models
{
    public class Papildoma_paslauga
    {
        public int id { get; set; }
        public string aprasymas { get; set; }
        public float kaina { get; set; }
        public int fk_Viesbutisid { get; set; }
    }
}