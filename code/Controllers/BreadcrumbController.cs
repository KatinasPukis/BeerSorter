using BeerSorter.Feature.Breadcrumb.Controllers.Base;
using BeerSorter.Feature.Breadcrumb.Models;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.Breadcrumb.Controllers
{
    public class BreadcrumbController : BaseController
    {


        // GET: Breadcrumb
        public override ActionResult Index()
        {
            var breadcrumbsList = GetBreadcrumbs(Context.Item);
            return View(breadcrumbsList);
        }
        public BreadcrumbViewModel GetBreadcrumbs(Item current)
        {
            var breadcrumbViewModel = new BreadcrumbViewModel()
            {
                SeperationCharacter = this.ContextItem[Templates.Breadcrumb.Fields.BreadcrumbSymbolID],
                BreadcrumbItems = GetBreadcrumbItems()
            };

            return breadcrumbViewModel;
        }

        private static List<BreadcrumbItemModel> GetBreadcrumbItems()
        {
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var current = Sitecore.Context.Item;
            var breadcrumbList = new List<BreadcrumbItemModel>();
            while (current != null)
            {
                // get the link
                var currentUrl = LinkManager.GetItemUrl(current);
                // get the title from the item's title field
                var pagetitle = current[Templates.Breadcrumb.Fields.PagetitleID];
                breadcrumbList.Add(new BreadcrumbItemModel
                {
                    PageTitle = pagetitle,
                    PageUrl = currentUrl
                });

                if (current.ID == homeItem.ID)
                    break;

                current = current.Parent;

            }
            breadcrumbList.Reverse();
            return breadcrumbList;
        }
    }
}