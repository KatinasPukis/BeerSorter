using BeerSorter.Feature.Rating.Models;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.Rating.Controllers
{
    public class RatingController : Controller
    {
        // GET: Rating
        public const string STAR_FIELD = "star";
        public const string REQUEST_METHOD_FIELD = "POST";
        public const string HIDDEN_INPUT_FIELD = "CommentSubmitted";
        public ActionResult Index()
        {
           
            CheckIfPost();
            var model = GetDataFromForm();

            return View(model);
        }
        private void CheckIfPost() //checks if the http request is post
        {
            if (HttpContext.Request.HttpMethod == REQUEST_METHOD_FIELD) // if post then move forwatd
            {
                if (Request[HIDDEN_INPUT_FIELD] == "true")
                {

                    CreateRating();
                }


            }
        }
        private void CreateRating()
        {
            var model = GetDataFromForm();
            System.Diagnostics.Debug.WriteLine(model.RatingNumber, model.Username);
        }
        private RatingModel GetDataFromForm()
        {

            NameValueCollection nvc = Request.Form;
            var model = new RatingModel();
            ViewData[STAR_FIELD] = model.RatingNumber;
            //if (!string.IsNullOrEmpty(nvc[STAR_FIELD]))
            //{
            //    model.RatingNumber = nvc[STAR_FIELD];
            //}
            model.RatingDate = Sitecore.DateUtil.IsoNow;
            

            model.Username = Sitecore.Context.User.Name.ToString(); // fix domain\username



            return model;
        }
    }
}