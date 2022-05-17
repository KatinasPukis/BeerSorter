using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerSorter.Feature.Breadcrumb.Models
{
    public class BreadcrumbViewModel
    {
        public List<BreadcrumbItemModel> BreadcrumbItems { get; set; }
        public string SeperationCharacter { get; set; }

    }
}