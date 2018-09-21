using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPPro.ViewModels
{
    public class EmplRightsViewModel
    {
        public int id { get; set; }

        [Display(Name = "Viešbutis")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Hotel { get; set; }

        [Display(Name = "Redagavimo teisės")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public bool RightsStatus { get; set; }

        public List<SelectListItem> ERigths { get; set; }
    }
}