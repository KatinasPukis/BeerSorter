using BeerSorter.Feature.Rating.Models;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerSorter.Feature.Rating.Controllers.Base;
using Sitecore.Data;
using BeerSorter.Foundation.Core.Extentions;

namespace BeerSorter.Feature.Rating.Controllers
{
    public class RatingController : BaseController
    {
        // GET: Rating
        public const string STAR_FIELD = "star";
        public const string REQUEST_METHOD_FIELD = "POST";
        public const string HIDDEN_INPUT_FIELD = "RatingSubmitted";
        public const string RATING_FOLDER_NAME_FIELD = "RatingFolder";
        public ActionResult Index()
        {
            CheckIfPost();


            return View();
        }
        private void CheckIfPost() //checks if the http request is post
        {


            if (HttpContext.Request.HttpMethod == REQUEST_METHOD_FIELD) // if post then move forwatd
            {

                if (Request[HIDDEN_INPUT_FIELD] == "true")
                {
                    ensureRatingFolderExsits(Templates.Rating.RatingFolderTemplateID);
                    CreateRating();

                }



            }

        }
        private void CreateRating()
        {
            var model = GetDataFromForm();
            var datasource = Sitecore.Context.Item; // datasource of the selected movie

            TemplateItem ratingTemplate = GetTemplate(Templates.Rating.RatingTemplateID); // template for the rating 
            Item ratingFolder = ContextItem.Axes.GetChild(ContextItem.Children.First(c => c.TemplateID.Equals(Templates.Rating.RatingFolderTemplateID)).Name);
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {

                Item rating = ratingFolder.Add((model.Username + "-" + model.RatingNumber).Trim(), ratingTemplate); // create a new rating item, the name should be username + date aded , and what template should be used in 
                try
                {
                    if (rating != null)
                    {
                        rating.Editing.BeginEdit();

                        rating[Templates.Rating.Fields.UsernameFieldID] = model.Username;

                        rating[Templates.Rating.Fields.RatingFieldID] = model.RatingNumber;


                        rating[Templates.Rating.Fields.DateAdded] = model.RatingDate;

                        rating.Editing.EndEdit();

                    }
                }
                catch (System.Exception ex)
                {
                    rating.Editing.CancelEdit();
                }




            }


        }
        private RatingModel GetDataFromForm()
        {

            NameValueCollection nvc = Request.Form;
            var model = new RatingModel();
            //ViewData[STAR_FIELD] = model.RatingNumber;
            if (!string.IsNullOrEmpty(nvc[STAR_FIELD]))
            {
                model.RatingNumber = nvc[STAR_FIELD];
            }
            model.RatingDate = Sitecore.DateUtil.IsoNow;

            string LoggedInUserName = Sitecore.Context.User.Name.ToString();

            string[] test = LoggedInUserName.Split('\\');

            model.Username = test[1]; // fix domain\username



            return model;
        }
        private static TemplateItem GetTemplate(Sitecore.Data.ID templateid)
        {
            return Sitecore.Context.Item.Database.GetTemplate(templateid);
        }
        private void ensureRatingFolderExsits(ID itemId)
        {
            List<Item> itemFolders = GetChildrenOfContextItemBasedOnTemplateID(itemId);

            if (itemFolders.Count == 0) // There is no existing contact form folder
            {
                CreateTemplateItem(RATING_FOLDER_NAME_FIELD, Templates.Rating.RatingFolderTemplateID, ContextItem);

            }
            else// There is more than one item, delete empty
            {
                // delete all empty folders
                CleanUpFolders(itemFolders);

            }
        }
        private void CleanUpFolders(List<Item> itemFolders)
        {
            if (itemFolders.Count() > 1)
            {
                MoveChildren(itemFolders.Skip(1).ToList(), itemFolders.First());
                foreach (var item in itemFolders.Where(c => !c.Children.Any()))
                {
                    item.Delete();
                }
            }
        }

        // Add template item with a certain name to the parent item
        private void CreateTemplateItem(string templateName, ID templateId, Item parentItem)
        {
            TemplateItem template = ContextItem.Database.GetTemplate(templateId);
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                parentItem.Add(templateName, template);
            }

        }

        private List<Item> GetChildrenOfContextItemBasedOnTemplateID(ID itemId)
        {
            return ContextItem.Children.Where(c => c.Template.IsDerived(itemId)).ToList();
        }

        private void MoveChildren(List<Item> itemFolders, Item DestinationFolder)
        {
            foreach (Item child in itemFolders.SelectMany(f => f.Children))
            {
                child.MoveTo(DestinationFolder);
            }
        }
    }
}