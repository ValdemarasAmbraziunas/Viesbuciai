using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.ViewModels
{
    public class ServiceViewModel
    {
        public int id { get; set; }

        [Display(Name = "Kaina")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public float Price { get; set; }

        [Display(Name = "Aprašymas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Description { get; set; }

    }
}