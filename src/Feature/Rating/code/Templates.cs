using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.Rating
{
    public class Templates
    {
        public static class Rating
        {
            public static readonly ID RatingTemplateID = new ID("{03BCB747-E7A9-4291-BFC2-01060162315A}");
            public static readonly ID RatingFolderTemplateID = new ID("{A0B56A45-9823-4B5E-83B9-C651D5906AFB}");
            public static readonly ID RatingDataFolderID = new ID("{67560B77-7094-4EF4-83E3-65E58A6B4C71}");
            public static class Fields
            {
                public static readonly ID UsernameFieldID = new ID("{FB43BAA5-F260-47EA-B51C-043976E2B53C}");
                public static readonly ID RatingFieldID = new ID("{5025796E-31A7-4529-BC85-BEAFAA471AA8}");
                public static readonly ID DateAdded = new ID("{6B5904AC-7A30-4BA5-80C6-8432C3D63326}");
            }

        }
    }
}