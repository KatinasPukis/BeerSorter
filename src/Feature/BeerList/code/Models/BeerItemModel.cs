﻿using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.BeerList.Models
{
    public class BeerItemModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string CountryOfOrigin { get; set; }
        public List<Item> Kind { get; set; }
        public string Price { get; set; }
        public string AlcoholStrenght { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Packaging { get; set; }
        public string Volume { get; set; }
        public string LinkToSelf { get; set; }

    }
}