using BeerSorter.Feature.Registration.Models;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BeerSorter.Feature.Registration.Controllers
{
    public class RegistrationController : Controller
    {
        // constants used to specify the cshtml form input names
        public const string EMAIL_FIELD = "email";
        public const string USERNAME_FIELD = "username";
        public const string NAME_FIELD = "name";
        public const string LASTNAME_FIELD = "lastname";
        public const string PASSWORD_FIELD = "password";
        public const string REPEATPASSWORD_FIELD = "repeatpassword";
        public const string REQUEST_METHOD_FIELD = "POST";
        public const string HIDDEN_INPUT_FIELD = "CommentSubmitted";

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

                    CreateUser();
                }


            }
        }
        // Simple code version no checks, just to test the user creation, roles not added
        private ActionResult CreateUser()
        {
            var model = GetDataFromForm();
            string domain = @"extranet";
            //string userName = string.Concat(model.Name, model.LastName);
            string userName = model.Username;
            userName = string.Format(@"{0}\{1}", domain, userName);
            try
            {
                string domainUser = Sitecore.Context.Domain.GetFullName(model.Email);
                Role role = Role.FromName(@"extranet\BeerUser");
                Membership.CreateUser(userName, model.Password, model.Email);
                Sitecore.Security.Accounts.User user = Sitecore.Security.Accounts.User.FromName(userName, true);
                user.Roles.Add(role);
                Sitecore.Security.UserProfile userProfile = user.Profile;
                userProfile.FullName=string.Format("{0} {1}", model.Name, model.LastName);
                userProfile.Email = model.Email;
                userProfile.Comment = "BeerSorter user";
                userProfile.Save();

            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(string.Format("Error in Client.Project.Security.UserMaintenance (AddUser): Message: {0}; Source:{1}", ex.Message, ex.Source), this);
            }
            return Redirect("https://beersortersc.dev.local/Login");

        }
        private UserRegistrationModel GetDataFromForm()
        {

            NameValueCollection nvc = Request.Form;
            var model = new UserRegistrationModel();

            if (!string.IsNullOrEmpty(nvc[USERNAME_FIELD]))
            {
                model.Username = nvc[USERNAME_FIELD];
            }

            if (!string.IsNullOrEmpty(nvc[EMAIL_FIELD]))

            {
                model.Email = nvc[EMAIL_FIELD];
            }

            if (!string.IsNullOrEmpty(nvc[NAME_FIELD]))
            {
                model.Name = nvc[NAME_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[LASTNAME_FIELD]))
            {
                model.LastName = nvc[LASTNAME_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[PASSWORD_FIELD]))
            {
                model.Password = nvc[PASSWORD_FIELD];
            }
            if (!string.IsNullOrEmpty(nvc[REPEATPASSWORD_FIELD]))
            {
                model.RepeatPassword = nvc[REPEATPASSWORD_FIELD];
            }



            return model;
        }

    }
}