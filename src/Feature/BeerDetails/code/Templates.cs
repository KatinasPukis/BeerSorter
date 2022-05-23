using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.BeerDetails
{
    public class Templates
    {
        public static class BeerDetails
        {
            public static readonly ID BeerTemplateID = new ID("{67560B77-7094-4EF4-83E3-65E58A6B4C71}");
            public static readonly ID BeerDataFolderID = new ID("{BA3E6595-40CC-493E-B906-D91F7733E02B}");
            public static class Fields
            {
                public static readonly ID NameFieldID = new ID("{DC69A056-9DE6-4B9F-8C0A-19A89FE40456}");
                public static readonly ID BrandFieldID = new ID("{BC01A279-9F09-4F42-937B-B5DF1DCF5A23}");
                public static readonly ID CountryOfOriginFieldID = new ID("{6F78FEFB-890B-47B9-A121-EBAC1E0508AC}");
                public static readonly ID KindFieldID = new ID("{149FCE7C-F8A5-4A43-838B-B83FAF48BEE7}");
                public static readonly ID PriceFieldID = new ID("{1779350A-ADF1-407A-872C-E0114036CB6B}");
                public static readonly ID AlcoholStrenghtFieldID = new ID("{7424661E-21EA-41FA-800D-74D096FE82AD}");
                public static readonly ID StyleFieldID = new ID("{6BCADEC6-7A7D-42EA-901D-B689F12A0CD0}");
                public static readonly ID DescriptionFieldID = new ID("{16FA4E16-FAAE-4E16-BB83-6808BD5C5CA0}");
                public static readonly ID ImageFieldID = new ID("{A1E76ABB-E5B8-4486-B93F-C945E07B7868}");
                public static readonly ID PackagingFieldID = new ID("{F22535EB-BF7D-4F76-B9C4-5471CA0C4267}");
                public static readonly ID VolumeFieldID = new ID("{CDAF7969-DDF2-4A95-AA83-94AF47CA28C3}");
                public static readonly ID KindTestFieldID = new ID("{CA82D138-07DB-4288-B5E0-F2D59BC5E973}");
            }
        }
    }
}