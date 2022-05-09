using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.Header.Models
{
    public class HeaderModel
    {
        public string LogoID { get; set; }
        public MenuViewModel Page { get; set; }
    }
}