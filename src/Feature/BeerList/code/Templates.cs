using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace BeerSorter.Feature.BeerList
{
    public class Templates
    {
        public static class BeerList
        {
            public static readonly ID BeerTemplateID = new ID("{67560B77-7094-4EF4-83E3-65E58A6B4C71}");
            public static readonly ID BeerDataFolderID = new ID("{BA3E6595-40CC-493E-B906-D91F7733E02B}");
            public static class Fields
            {
                //public static readonly string NameFieldID = "Name";
                public static readonly ID NameFieldID = new ID("{DC69A056-9DE6-4B9F-8C0A-19A89FE40456}");
                public static readonly ID BrandFieldID = new ID("{BC01A279-9F09-4F42-937B-B5DF1DCF5A23}");
                public static readonly ID PriceFieldID = new ID("{1779350A-ADF1-407A-872C-E0114036CB6B}");
                public static readonly ID PackagingFieldID = new ID("{F22535EB-BF7D-4F76-B9C4-5471CA0C4267}");
                public static readonly ID DescriptionFieldID = new ID("{16FA4E16-FAAE-4E16-BB83-6808BD5C5CA0}");

            }
        }
    }
}