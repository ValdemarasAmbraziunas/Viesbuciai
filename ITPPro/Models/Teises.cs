using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITPPro.Models.Enums;
namespace ITPPro.Models
{
    public class Teises
    {
        public int id { get; set; }
        public string viesbuciu_tinklas { get; set; }
        public string viesbutis { get; set; }
        public bool teisiu_statusas { get; set; }
        public virtual Teisiu_Tipo_Enum tipas { get; set; }
        public int? fk_Klientaskliento_kodas { get; set; }
        public int? fk_Darbuotojasdarbuojo_kodas { get; set; }
        public string priezastis { get; set; }
        public DateTime data_iki { get; set; }
    }
}