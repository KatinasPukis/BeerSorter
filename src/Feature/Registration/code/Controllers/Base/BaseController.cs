using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.Registration.Controllers.Base
{
    public class BaseController : Controller
    { 
        public Item CurrentItem
        {
            get
            {
                var dataSource = RenderingContext.CurrentOrNull?.Rendering.Item;
                return dataSource ?? Sitecore.Context.Item;
            }

        }

    }
}