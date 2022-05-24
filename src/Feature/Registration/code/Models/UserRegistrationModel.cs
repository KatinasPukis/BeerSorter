using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.Registration.Models
{
    public class UserRegistrationModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}