using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spotters.Service.ViewModels
{
    public class PlaneSpotterViewModel: BaseViewModel
    {
        [Required]
        [MaxLength(128)]
        public String Make { get; set; }
        [Required]
        [MaxLength(128)]
        public String Model { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9]{1,2}-[a-zA-Z0-9]{1,5})$")]
        public String Registration { get; set; }
        [Required]
        [MaxLength(255)]
        public String Location { get; set; }
        [Required]
        public DateTime DateandTime { get; set; }
        public IFormFile SpotterImage { get; set; }
        public String SpotterImageUrl { get; set; }
    }
}
