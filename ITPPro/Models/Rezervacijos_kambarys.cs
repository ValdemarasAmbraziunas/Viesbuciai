using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITPPro.Models
{
    public class Rezervacijos_kambarys
    {
        [Key]
        [Column(Order = 1)]
        public int fk_Kambarysid { get; set; }
        [Key]
        [Column(Order = 2)]
        public int fk_Rezervacijaid { get; set; }
    }
}