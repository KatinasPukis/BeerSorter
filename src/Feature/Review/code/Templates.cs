using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.Review
{
    public class Templates
    {
        public static class Review
        {
            public static readonly ID ReviewTemplateID = new ID("{7893AD15-9718-47D5-9F74-476C39EE4A9C}");
            public static readonly ID ReviewFolderTemplateID = new ID("{6FE6EAD5-061A-4166-BB4D-F372CE1345D4}");
            public static readonly ID ReviewDataFolderID = new ID("{BA3E6595-40CC-493E-B906-D91F7733E02B}");
            public static class Fields
            {
                //public static readonly ID EmailFieldID = new ID("{493E672A-0E37-4FBF-A692-8EF9A086F088}");
                public static readonly ID UsernameFieldID = new ID("{6EA77825-EF83-47DC-A49B-96157815EDDF}");
                public static readonly ID CommentFieldID = new ID("{E3924006-D8B0-4BC6-BFF2-687E89194220}");
                public static readonly ID DateAdded = new ID("{13A7C91B-0C8A-4408-A124-F7C57AF72958}");
                public static readonly ID RatingFieldID = new ID("{A265DEC2-371C-4DC8-977B-D74FFFE86FBA}");
            }

        }
    }
}