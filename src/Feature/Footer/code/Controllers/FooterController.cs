using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Data;
using BeerSorter.Feature.Footer.Controllers.Base;
using Sitecore.Collections;
using System.Collections.Generic;
using BeerSorter.Feature.Footer.Models;

namespace BeerSorter.Feature.Footer.Controllers
{
    public class FooterController : BaseController
    {
        public ActionResult Index()
        {
            var footerModel = new FooterModel
            {
                Images = GetImages(),
                Links = GetLinkFields()
            };



            return View(footerModel);
        }

        private List<Item> GetLinkFields()
        {
            List<Item> footerLinksFolders = ContextItem.Axes.GetDescendants().Where(d => d.TemplateID.Equals(Templates.Footer.FooterLinksFolder)).ToList();

            return footerLinksFolders;
        }

        private List<Item> GetImages()
        {
            List<Item> footerImagesFolders = ContextItem.Axes.GetDescendants().Where(m => m.TemplateID.Equals(Templates.Footer.FooterImagesFolder)).ToList();

            return footerImagesFolders;
        }
    }
}