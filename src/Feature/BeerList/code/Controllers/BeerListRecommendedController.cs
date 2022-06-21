using BeerSorter.Feature.BeerList.Models;
using BeerSorter.Foundation.Core.Extentions;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.BeerList.Controllers
{
    public class BeerListRecommendedController : Controller
    {
        // GET: BeerListRecommended
        public ActionResult Index()
        {
            var beerList = GetBeerList();
            return View(beerList);
        }
        private BeerViewModel GetBeerList()
        {
            var beerModelList = new BeerViewModel
            {
                BeerItems = GetBeers()
            };
            return beerModelList;
        }

        private List<BeerItemModel> GetBeers()
        {

            var beerList = new List<BeerItemModel>();

            var datasource = RenderingContext.CurrentOrNull.Rendering.Item;
            

            foreach (Item item in Sitecore.Context.Item.Children.Where(c => c.IsItemDerived(Templates.BeerList.BeerTemplateID)))
            {
                if(item.Fields[Templates.BeerList.Fields.StyleFieldID].Value==datasource.Name.ToString())
                {
                    beerList.Add(AddBeerItemModel(item));
                }
                
            }
            return beerList;
        }
        private BeerItemModel AddBeerItemModel(Item beer)
        {
            ImageField imgField = beer.Fields[Templates.BeerList.Fields.ImageFieldID];
            var imagepath = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
            string LinkToSelf = Sitecore.Links.LinkManager.GetItemUrl(beer);
            return new BeerItemModel
            {
                Name = beer[Templates.BeerList.Fields.NameFieldID],
                Brand = beer[Templates.BeerList.Fields.BrandFieldID],
                CountryOfOrigin = beer[Templates.BeerList.Fields.CountryOfOriginFieldID],
                Kind = beer[Templates.BeerList.Fields.KindFieldID],
                Price = beer[Templates.BeerList.Fields.PriceFieldID],
                AlcoholStrenght = Double.Parse(beer[Templates.BeerList.Fields.AlcoholStrenghtFieldID]),
                Style = beer[Templates.BeerList.Fields.StyleFieldID],
                Description = beer[Templates.BeerList.Fields.DescriptionFieldID],
                Image = imagepath,
                Packaging = beer[Templates.BeerList.Fields.PackagingFieldID],
                Volume = Double.Parse(beer[Templates.BeerList.Fields.VolumeFieldID]),
                LinkToSelf = LinkToSelf,
                Rating = beer[Templates.BeerList.Fields.RatingFieldID]

            };
        }
    }
}