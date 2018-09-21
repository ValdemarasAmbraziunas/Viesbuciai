using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.ViewModels
{
    public class RoomResult
    {
        [Display(Name = "Numeris")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Number { get; set; }

        [Display(Name = "Užimtumas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public double Busyness { get; set; }
    }
}