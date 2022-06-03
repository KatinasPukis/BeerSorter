using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.Review.Models
{
    public class ReviewModel
    {
        public string Username { get; set; }
        public string Comment { get; set; }
        public string DateAdded { get; set; }
    }
}