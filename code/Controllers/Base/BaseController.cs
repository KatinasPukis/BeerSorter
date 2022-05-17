using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;

namespace BeerSorter.Feature.Breadcrumb.Controllers.Base
{
    public class BaseController : SitecoreController
    {
        public Item ContextItem
        {
            get
            {
                var dataSource = RenderingContext.CurrentOrNull?.Rendering.Item;
                return dataSource ?? Sitecore.Context.Item;
            }

        }

    }
}