using BeerSorter.Feature.Login.Models;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerSorter.Feature.Login.Controllers
{
    public class LoginController : Controller
    {
        public const string USERNAME_FIELD = "username";
        public const string PASSWORD_FIELD = "password";
        public const string HIDDEN_INPUT_FIELD = "CommentSubmitted";
        public const string REQUEST_METHOD_FIELD = "POST";
        // GET: Login
        public ActionResult Index()
        {
            if(CheckIfPost()==true)
            {
                Logout();
                var user = UserLogin();
            }
            
            return View();
        }
        private void Logout()
        {
            AuthenticationManager.Logout();
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
        private User UserLogin()
        {

            var model = GetDataFromForm();
            var accountName = string.Empty;
            var domain = Sitecore.Context.Domain;
            if (domain != null)
            {
                accountName = domain.GetFullName(model.Username);
            }
            var result = AuthenticationManager.Login(accountName, model.Password);
            if (!result)
            {
                return null;
            }
            var user = AuthenticationManager.GetActiveUser();
            return user;
        }
        private UserLoginModel GetDataFromForm()
        {

            NameValueCollection nvc = Request.Form;
            var model = new UserLoginModel();


            if (!string.IsNullOrEmpty(nvc[USERNAME_FIELD]))

            {
                model.Username = nvc[USERNAME_FIELD];
            }



            if (!string.IsNullOrEmpty(nvc[PASSWORD_FIELD]))
            {
                model.Password = nvc[PASSWORD_FIELD];
            }



            return model;
        }

    }
}