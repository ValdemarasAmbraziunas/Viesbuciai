using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITPPro.Models
{
    public class Rezervacijos_papildoma_paslauga
    {
        [Key]
        [Column(Order = 1)]
        public int fk_Rezervacijaid { get; set; }
        [Key]
        [Column(Order = 2)]
        public int fk_Papildoma_paslaugaid { get; set; }
    }
}