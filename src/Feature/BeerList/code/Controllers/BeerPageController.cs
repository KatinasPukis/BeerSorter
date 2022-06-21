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
        public const string COUNTRYOFORIGIN_FIELD = "CountryOfOrigin";
        public const string RATING_FIELD = "Rating";
        public const string HIDDEN_INPUT_FIELD = "FilterSubmitted";
        public const string REQUEST_METHOD_FIELD = "POST";
        // GET: BeerPage
        public ActionResult Index()
        {
            if (CheckIfPost() == true)
            {
                var beerdata = GetDataFromForm();
                var beerList = GetBeerList();
                if (beerdata != null)
                {
                    beerList = FilterBeer(beerList, beerdata);

                }
                return View(beerList);
            }
            else
            {
                var beerList = GetBeerList();
                return View(beerList);
            }
        }
        private BeerViewModel FilterBeer(BeerViewModel beerList, BeerItemModel searchbeer)
        {
            var newList = new BeerViewModel();
            var beerItemList = new List<BeerItemModel>();
            try
            {
                foreach (var item in beerList.BeerItems)
                {
                    beerItemList.Add(item);
                }
                if(searchbeer.AlcoholStrenght != 0)
                {
                    string[] identifications = searchbeer.AlcoholStrenght.ToString().Split(new[] { "," }, StringSplitOptions.None);
                    double test1 = searchbeer.AlcoholStrenght / 10; // first number
                    double test2 = searchbeer.AlcoholStrenght % 10; // second number

                    beerItemList = beerItemList.Where(x => x.AlcoholStrenght >= test1).ToList();
                    beerItemList = beerItemList.Where(x => x.AlcoholStrenght <= test2).ToList();
                }
                if (searchbeer.VolumeList != null)
                {
                    string[] identifications = searchbeer.VolumeList.Split(new[] { "," }, StringSplitOptions.None);
                    double test1 = Double.Parse(identifications[0]); // first number
                    double test2 = Double.Parse(identifications[1]); // second number
                    searchbeer.Volume = Double.Parse(identifications[0]);
                    beerItemList = beerItemList.Where(x => x.Volume >= test1).ToList();
                    beerItemList = beerItemList.Where(x => x.Volume <= test2).ToList();
                }
                if (searchbeer.Style != null)
                {

                    beerItemList = beerItemList.Where(x => x.Style == searchbeer.Style).ToList();
                }
                if (searchbeer.Packaging != null)
                {
                    beerItemList = beerItemList.Where(x => x.Packaging == searchbeer.Packaging).ToList();
                }
                if(searchbeer.CountryOfOrigin != null)
                {
                    beerItemList = beerItemList.Where(x => x.CountryOfOrigin == searchbeer.CountryOfOrigin).ToList();
                }
                if (searchbeer.Kind != null)
                {
                    beerItemList = beerItemList.Where(x => x.Kind == searchbeer.Kind).ToList();
                }
                if (searchbeer.Rating != null)
                {
                    string[] identifications = searchbeer.Rating.Split(new[] { "," }, StringSplitOptions.None);
                    double ratingFrom = Double.Parse(identifications[0]);
                    double ratingTo = Double.Parse(identifications[1]);
                    beerItemList = beerItemList.Where(x => Double.Parse(x.Rating) >= ratingFrom).ToList();
                    beerItemList = beerItemList.Where(x => Double.Parse(x.Rating) <= ratingTo).ToList();

                }
                newList.BeerItems = beerItemList;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("error", ex);
            }
            return newList;
        }
        private bool CheckIfPost() //checks if the http request is post
        {
            if (HttpContext.Request.HttpMethod == REQUEST_METHOD_FIELD) // if post then move forwatd
            {
                return true;
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
            if (!string.IsNullOrEmpty(nvc[KIND_FIELD]))
            {

                model.Kind = nvc[KIND_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[ALCOHOLSTRENGHT_FIELD]))
            {
                model.AlcoholStrenght = Double.Parse(nvc[ALCOHOLSTRENGHT_FIELD]);
            }
            if (!string.IsNullOrEmpty(nvc[VOLUME_FIELD]))
            {
                model.VolumeList = nvc[VOLUME_FIELD];
                //model.Volume = Double.Parse(nvc[VOLUME_FIELD]); //problema nes gauni 0.10.2 stringa
            }
            if (!string.IsNullOrEmpty(nvc[PACKAGING_FIELD]))
            {
                model.Packaging = nvc[PACKAGING_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[COUNTRYOFORIGIN_FIELD]))
            {
                model.CountryOfOrigin = nvc[COUNTRYOFORIGIN_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[RATING_FIELD]))
            {
                model.Rating = nvc[RATING_FIELD];
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
                //Math.Round(Double.Parse(beer[Templates.BeerList.Fields.RatingFieldID]),2).ToString()

            };
        }
    }
}