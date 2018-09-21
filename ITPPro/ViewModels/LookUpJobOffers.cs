using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPPro.ViewModels
{
    public class LookUpJobOffers
    {
        public int id { get; set; }
        [Display(Name = "Pareigos")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Job { get; set; }


        [Display(Name = "Viešbucių tinklas siulantis darbą")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string HotelNet { get; set; }

        public int darbdavio_id { get; set; }

        public List<SelectListItem> Hotels { get; set; }
    }
}