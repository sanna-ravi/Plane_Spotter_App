using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Data.Models
{
    public class PlaneSpotter: BaseEntity
    {
        public String Make { get; set; }
        public String Model { get; set; }
        public String Registration { get; set; }
        public String Location { get; set; }
        public DateTime DateandTime { get; set; }
        public String SpotterImageUrl { get; set; }
    }
}
