using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PagedList;

namespace ITPPro.ViewModels
{
    public class HotelsViewModel
    {
        public int id { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Title { get; set; }

        [Display(Name = "Viešbučių tinklas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string HotelsNet { get; set; }

        [Display(Name = "Žvaigždučių skaičius")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public int Stars { get; set; }

        [Display(Name = "Miestas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string City { get; set; }

        [Display(Name = "Adresas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Address { get; set; }

        [Display(Name = "Aprašymas")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public string Description { get; set; }

        public bool Rights { get; set; }
    
        public RoomViewModel RoomsTitle { get; set; }
        public ServiceViewModel ServicesTitle { get; set; }

        public IPagedList<HotelsViewModel> hotels { get; set; }

        public List<RoomViewModel> Rooms { get; set; }
        public List<ServiceViewModel> Services { get; set; }
        public List<SelectListItem> Hotels { get; set; }

    }
}