using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool IsDisabled { get; set; }
    }
}
