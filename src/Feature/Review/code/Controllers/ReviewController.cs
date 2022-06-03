using BeerSorter.Feature.Review.Controllers.Base;
using BeerSorter.Feature.Review.Models;
using BeerSorter.Foundation.Core.Extentions;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.Review.Controllers
{
    public class ReviewController : BaseController
    {
        public const string USERNAME_FIELD = "username";
        public const string COMMENT_FIELD = "comment";
        public const string COMMENT_FOLDER_NAME_FIELD = "ReviewFolder";
        public const string REQUEST_METHOD_FIELD = "POST";
        public const string HIDDEN_INPUT_FIELD = "CommentSubmitted";
        // GET: Review
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
                    ensureCommentFolderExsits(Templates.Review.ReviewFolderTemplateID);
                    CreateComment();
                }


            }
        }
        private void CreateComment()
        {
            var model = GetDataFromForm(); // the comment model variable

            var datasource = Sitecore.Context.Item; // datasource of the selected movie

            TemplateItem commentTemplate = GetTemplate(Templates.Review.ReviewTemplateID); // template for the comment 

            Item commentFolder = ContextItem.Axes.GetChild(ContextItem.Children.First(c => c.TemplateID.Equals(Templates.Review.ReviewFolderTemplateID)).Name); // gets the first child name of the comment folder template by id.

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {

                Item comment = commentFolder.Add((model.Username + "-" + Sitecore.DateUtil.IsoNow).Trim(), commentTemplate); // create a new comment item, the name should be username + date aded , and what template should be used in 
                try
                {
                    if (comment != null)
                    {
                        comment.Editing.BeginEdit();


                        comment[Templates.Review.Fields.UsernameFieldID] = model.Username;

                        comment[Templates.Review.Fields.CommentFieldID] = model.Comment;

                        comment[Templates.Review.Fields.DateAdded] = model.DateAdded;

                        comment.Editing.EndEdit();

                    }
                }
                catch (System.Exception ex)
                {
                    comment.Editing.CancelEdit();
                }




            }

        }
        private static TemplateItem GetTemplate(Sitecore.Data.ID templateid)
        {
            return Sitecore.Context.Item.Database.GetTemplate(templateid);
        }

        public ReviewModel GetDataFromForm()
        {

            NameValueCollection nvc = Request.Form;
            var model = new ReviewModel();

            if (!string.IsNullOrEmpty(nvc[USERNAME_FIELD]))
            {
                model.Username = nvc[USERNAME_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[COMMENT_FIELD]))
            {
                model.Comment = nvc[COMMENT_FIELD];
            }

            model.DateAdded = Sitecore.DateUtil.IsoNow;

            return model;
        }
        private void ensureCommentFolderExsits(ID itemId)
        {
            List<Item> itemFolders = GetChildrenOfContextItemBasedOnTemplateID(itemId);

            if (itemFolders.Count == 0) // There is no existing contact form folder
            {
                CreateTemplateItem(COMMENT_FOLDER_NAME_FIELD, Templates.Review.ReviewFolderTemplateID, ContextItem);

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