using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ITPPro.ViewModels
{
    public class CreateJobOfferModel
    {
        [Display(Name = "Pareigos")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Job { get; set; }
        [Display(Name = "Slaptažodis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Password { get; set; }

        [Display(Name = "Pakartokite slaptažodį")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string RepeatPassword { get; set; }

        public int kliento_id { get; set; }
        public int darbdavio_id { get; set; }
    }
}