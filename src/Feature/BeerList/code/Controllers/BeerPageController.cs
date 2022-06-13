using BeerSorter.Feature.BeerDetails.Controllers.Base;
using BeerSorter.Feature.BeerList.Models;
using BeerSorter.Foundation.Core.Extentions;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.BeerList.Controllers
{
    public class BeerPageController : BaseController
    {
        public const string KIND_FIELD = "Kind";
        public const string STYLE_FIELD = "Style";
        public const string ALCOHOLSTRENGHT_FIELD = "AlcoholStrenght";
        public const string VOLUME_FIELD = "Volume";
        public const string PACKAGING_FIELD = "Packaging";
        public const string HIDDEN_INPUT_FIELD = "FilterSubmitted";
        public const string REQUEST_METHOD_FIELD = "POST";
        // GET: BeerPage
        public ActionResult Index()
        {

            if (CheckIfPost() == true)
            {
                var beerdata = GetDataFromForm();
                var beerList = GetBeerList();
                beerList = FilterBeer(beerList, beerdata);
                return View(beerList);
            }
            else
            {
                var beerList = GetBeerList();
                return View(beerList);
            }
        }
        private BeerViewModel FilterBeer(BeerViewModel beers, BeerItemModel searchbeer)
        {
            var newList = new BeerViewModel();

            foreach (var item in beers.BeerItems)
            {
                if (searchbeer.Packaging == item.Packaging)
                {
                    newList.BeerItems.Add(item);
                }
            }

            return newList;
        }
        private bool CheckIfPost() //checks if the http request is post
        {
            if (HttpContext.Request.HttpMethod == REQUEST_METHOD_FIELD) // if post then move forwatd
            {
                if (Request[HIDDEN_INPUT_FIELD] == "true")
                {

                    return true;
                }


            }
            return false;
        }
        private BeerItemModel GetDataFromForm()
        {

            NameValueCollection nvc = Request.Form;
            var model = new BeerItemModel();


            if (!string.IsNullOrEmpty(nvc[STYLE_FIELD]))

            {
                model.Style = nvc[STYLE_FIELD];
            }

            if (!string.IsNullOrEmpty(nvc[ALCOHOLSTRENGHT_FIELD]))
            {
                model.AlcoholStrenght = nvc[ALCOHOLSTRENGHT_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[VOLUME_FIELD]))
            {
                model.Volume = nvc[VOLUME_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[PACKAGING_FIELD]))
            {
                model.Packaging = nvc[PACKAGING_FIELD];
            }
            return model;
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


            foreach (Item item in Sitecore.Context.Item.Children.Where(c => c.IsItemDerived(Templates.BeerList.BeerTemplateID)))
            {
                beerList.Add(AddBeerItemModel(item));
            }
            return beerList;
        }
        private List<Item> GetKind(Item item)
        {
            List<Item> kinds = new List<Item>();
            string[] identifications = item[Templates.BeerList.Fields.KindFieldID].Split(new[] { "|" }, StringSplitOptions.None);
            foreach (var id in identifications)
            {
                kinds.Add(Sitecore.Context.Database.GetItem(id));
            }

            return kinds;

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
                Kind = GetKind(beer),
                Price = beer[Templates.BeerList.Fields.PriceFieldID],
                AlcoholStrenght = beer[Templates.BeerList.Fields.AlcoholStrenghtFieldID],
                Style = beer[Templates.BeerList.Fields.StyleFieldID],
                Description = beer[Templates.BeerList.Fields.DescriptionFieldID],
                Image = imagepath,
                Packaging = beer[Templates.BeerList.Fields.PackagingFieldID],
                Volume = beer[Templates.BeerList.Fields.VolumeFieldID],
                LinkToSelf = LinkToSelf,

            };
        }
        public class BeerSearch
        {
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Kind { get; set; }
            public string Style { get; set; }
            public string AlcoholStrenght { get; set; }
            public string Packaging { get; set; }
            public string Volume { get; set; }
        }

    }
}