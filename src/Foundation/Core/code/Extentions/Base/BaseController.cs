using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Foundation.Core.Extentions.Base
{
    public class BaseController : Controller
    {
        public Item ContextItem
        {
            get
            {
                var dataSource = RenderingContext.CurrentOrNull.Rendering.Item;
                return dataSource ?? Sitecore.Context.Item;
            }
        }
    }
}