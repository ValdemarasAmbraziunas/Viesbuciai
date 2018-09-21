using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ITPPro.ViewModels
{
    public class ValdytojoRegictracijosViewModel
    {
        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Name { get; set; }

        [Display(Name = "Pavardė")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Surname { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Password { get; set; }

        [Display(Name = "Pakartokite slaptažodį")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string RepeatPassword { get; set; }

        [Display(Name = "Adresas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Address { get; set; }

        [Display(Name = "Telefonas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Phone { get; set; }

        [Display(Name = "Lytis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Gender { get; set; }

        [Display(Name = "El. paštas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Email { get; set; }


    }
}