using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.ViewModels
{
    public class RestrictRightsViewModel
    {
        [Display(Name = "Priežastis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Reason { get; set; }

        [Display(Name = "Data iki kurio laiko bus apribotos teisės")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public DateTime DateEnd { get; set; }
    }
}