using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPPro.ViewModels
{
    public class EmpViewModel
    {
        public int id { get; set; }

        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Name { get; set; }

        [Display(Name = "Pavardė")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Surname { get; set; }

        [Display(Name = "Telefonas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Phone { get; set; }

        [Display(Name = "El. paštas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Email { get; set; }

        [Display(Name = "Adresas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Address { get; set; }

        [Display(Name = "Lytis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Gender { get; set; }

        [Display(Name = "Pareigos")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Role { get; set; }

        [Display(Name = "Darbo pradžios laikas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public DateTime  StartTime { get; set; }

        public List<SelectListItem> Empl { get; set; }
    }
}