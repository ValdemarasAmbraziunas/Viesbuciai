using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.ViewModels
{
    public class AcceptJobOffer
    {
        [Display(Name = "Slaptažodis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Password { get; set; }

        [Display(Name = "Pakartokite slaptažodį")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string RepeatPassword { get; set; }

    }
}