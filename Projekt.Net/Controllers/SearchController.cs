using Projekt.Net.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Net.Controllers
{
    public class SearchController : Controller
    {
        //Här är vår söksida. Den är tom som default.
        // GET: Search
        public ActionResult Index()
        {
            Search Model = new Search();
            DBRep DB = new DBRep();

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("BortaMatchCookie"))
            {
                int cookieID = Convert.ToInt32(this.ControllerContext.HttpContext.Request.Cookies["BortaMatchCookie"].Value);
                var user = DB.getUser(cookieID) as Användare;
                var profile = DB.getProfile(cookieID) as ProfilInfo;
                List<FriendRequest> requests = DB.getFriendRequests(cookieID);

                Model.HeaderModel.FirstName = profile.FNamn;
                Model.HeaderModel.Username = user.UName;
                Model.HeaderModel.Password = user.PW;
                Model.HeaderModel.FriendRequestList = requests;
                Model.HeaderModel.UserID = cookieID;
                Model.HeaderModel.Inloggad = true;
            }
            else
            {
                Model.HeaderModel.Inloggad = false;
            }

            if (!Model.HeaderModel.Inloggad)
            {
                return RedirectToAction("LogIn", "Start");
            }

            return View(Model);
        }

        


        //Denna sida körs när man skrivit in något och man sedan vill klicka på sök-knappen. 
        [HttpPost]
        public ActionResult Index(Search Result)
        {
            DBRep DB = new DBRep();
            Search Model = new Search();

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("BortaMatchCookie"))
            {
                int cookieID = Convert.ToInt32(this.ControllerContext.HttpContext.Request.Cookies["BortaMatchCookie"].Value);
                var user = DB.getUser(cookieID) as Användare;
                var profile = DB.getProfile(cookieID) as ProfilInfo;
                List<FriendRequest> requests = DB.getFriendRequests(cookieID);

                Model.HeaderModel.FirstName = profile.FNamn;
                Model.HeaderModel.Username = user.UName;
                Model.HeaderModel.Password = user.PW;
                Model.HeaderModel.FriendRequestList = requests;
                Model.HeaderModel.UserID = cookieID;
                Model.HeaderModel.Inloggad = true;
            }
            else
            {
                Model.HeaderModel.Inloggad = false;
            }

            var Temp = Result.SökOrd;
            var Listan = DB.SearchResult(Temp);

            foreach(var item in Listan)
            {
                ProfilInfo temp = new ProfilInfo();
                temp.URL = DB.getProfile(item.UserID).URL;
                temp.UID = item.UserID;
                Model.URLListan.Add(temp);
            }

            Model.UserListan = Listan;

            return View(Model);
        }
    }
}