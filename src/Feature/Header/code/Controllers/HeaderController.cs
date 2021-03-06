using BeerSorter.Feature.Header.Models;
using Sitecore.Security.Authentication;
using System.Web.Mvc;

namespace BeerSorter.Feature.Header.Controllers
{
    public class HeaderController : Controller
    {
        // GET: Header
        public ActionResult Index()
        {
            var homeItem = Sitecore.Context.Database.GetItem(Templates.Home.HomeItemID);
            var headerModel = new HeaderModel
            {
                Page = new MenuViewModel(homeItem),
                LogoID = Templates.Header.Fields.LogoFieldID.ToString()
            };
            

            return View(headerModel);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return Redirect("/Beers");
        }
    }
}