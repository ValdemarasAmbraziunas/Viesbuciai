using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.ViewModels
{
    public class RoomViewModel
    {
        public int id { get; set; }

        [Display(Name = "Vietų skaičius")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public int Capacity { get; set; }

        [Display(Name = "Numeris")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Number { get; set; }

        [Display(Name = "Kaina parai")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public float Price { get; set; }

        [Display(Name = "Aprašymas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Description { get; set; }

        [Display(Name = "Tipas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Type { get; set; }
    }
}