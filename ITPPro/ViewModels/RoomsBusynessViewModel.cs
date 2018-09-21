using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITPPro.ViewModels
{
    public class RoomsBusynessViewModel
    {
        public int hotelid {get; set;}
        [Display(Name = "Data nuo")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Data iki")]
        [Required(ErrorMessage = "Šį lauką privoloma užpildyti")]
        public DateTime EndTime { get; set; }

    }
}