using BeerSorter.Feature.BeerDetails.Controllers.Base;
using BeerSorter.Feature.BeerDetails.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.BeerDetails.Controllers
{
    public class BeerDetailsController : BaseController
    {
        public ActionResult Index()
        {
            var beerModel = new BeerModel
            {
                Name = CurrentItem[Templates.BeerDetails.Fields.NameFieldID],
                Brand = CurrentItem[Templates.BeerDetails.Fields.BrandFieldID],
                CountryOfOrigin = CurrentItem[Templates.BeerDetails.Fields.CountryOfOriginFieldID],
                Kind = GetKind(),
                Price = CurrentItem[Templates.BeerDetails.Fields.PriceFieldID],
                AlcoholStrenght = CurrentItem[Templates.BeerDetails.Fields.AlcoholStrenghtFieldID],
                Style = CurrentItem[Templates.BeerDetails.Fields.StyleFieldID],
                Description = CurrentItem[Templates.BeerDetails.Fields.DescriptionFieldID],
                Image = GetImageUrl(CurrentItem),
                Packaging = CurrentItem[Templates.BeerDetails.Fields.PackagingFieldID],
                Volume = CurrentItem[Templates.BeerDetails.Fields.VolumeFieldID]
            };

            return View(beerModel);
        }
        private string GetImageUrl(Item currentItem)
        {
            ImageField imgField = currentItem.Fields[Templates.BeerDetails.Fields.ImageFieldID];


            return Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
        }
        private List<Item> GetKind()
        {
            List<Item> kinds = new List<Item>();
            string[] identifications = CurrentItem[Templates.BeerDetails.Fields.KindFieldID].Split(new[] {"|"},StringSplitOptions.None);
            foreach (var id in identifications)
            {
                kinds.Add(Sitecore.Context.Database.GetItem(id));
            }

            return kinds;

        }
    }
}