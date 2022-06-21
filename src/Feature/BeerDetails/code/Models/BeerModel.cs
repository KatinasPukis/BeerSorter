using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.BeerDetails.Models
{
    public class BeerModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Kind { get; set; }
        public string Price { get; set; }
        public string AlcoholStrenght { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Packaging { get; set; }
        public string Volume { get; set; }
        public double Rating { get; set; }


    }
}