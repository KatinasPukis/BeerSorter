using BeerSorter.Feature.BeerDetails.Controllers.Base;
using BeerSorter.Feature.BeerDetails.Models;
using BeerSorter.Foundation.Core.Extentions;
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
                Kind = CurrentItem[Templates.BeerDetails.Fields.KindFieldID],
                Price = CurrentItem[Templates.BeerDetails.Fields.PriceFieldID],
                AlcoholStrenght = CurrentItem[Templates.BeerDetails.Fields.AlcoholStrenghtFieldID],
                Style = CurrentItem[Templates.BeerDetails.Fields.StyleFieldID],
                Description = CurrentItem[Templates.BeerDetails.Fields.DescriptionFieldID],
                Image = GetImageUrl(CurrentItem),
                Packaging = CurrentItem[Templates.BeerDetails.Fields.PackagingFieldID],
                Volume = CurrentItem[Templates.BeerDetails.Fields.VolumeFieldID],
                Rating = GetRating(CurrentItem)
            };

            return View(beerModel);
        }
        private string GetImageUrl(Item currentItem)
        {
            ImageField imgField = currentItem.Fields[Templates.BeerDetails.Fields.ImageFieldID];


            return Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
        }
        private double GetRating(Item currentItem)
        {

            double OverallRating = 0;
            double tempRating = 0;
            double Childrencount = 0;
            try
            {
                foreach (Item reviewfolder in currentItem.Children.Where(c => c.IsItemDerived(Templates.ReviewDetails.ReviewFolderID)))
                {
                    foreach (Item review in reviewfolder.Children)
                    {
                        if (reviewfolder.Children.Count != 0)
                        {
                            // jeigu nera rating sulusta
                            tempRating = tempRating + Double.Parse(review[Templates.ReviewDetails.Fields.RatingFieldID]);
                            Childrencount = reviewfolder.Children.Count;
                        }

                    }
                }
                if (tempRating != 0 && Childrencount != 0)
                {
                    OverallRating = tempRating / Childrencount;

                }
                else
                {
                    OverallRating = 0;
                }
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    currentItem.Editing.BeginEdit();

                    try
                    {
                        currentItem[Templates.BeerDetails.Fields.RatingFieldID] = OverallRating.ToString();
                        currentItem.Editing.EndEdit();
                    }
                    catch (Exception ex)
                    {
                        currentItem.Editing.CancelEdit();
                    }
                }
                return Math.Round(OverallRating, 2);

            }
            catch (Exception ex)
            {

            }
            return Math.Round(OverallRating, 2);

        }
        //private List<Item> GetKind()
        //{
        //    List<Item> kinds = new List<Item>();
        //    string[] identifications = CurrentItem[Templates.BeerDetails.Fields.KindFieldID].Split(new[] {"|"},StringSplitOptions.None);
        //    foreach (var id in identifications)
        //    {
        //        kinds.Add(Sitecore.Context.Database.GetItem(id));
        //    }

        //    return kinds;

        //}
    }
}