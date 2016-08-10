using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using Projekt.Net.Models;

namespace Projekt.Net.Controllers
{
    public class StartController : Controller
    {
        //Här är vår startsida med lite go information.
        // GET: Start
        public ActionResult Index()
        {
            DBRep DB = new DBRep();
            Start Model = new Start();

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

            var Anv = DB.getRandomUsers();

            foreach (var item in Anv)
            {
                RandomBild tempBild = new RandomBild();
                var tempProfile = DB.getProfile(item.UserID);

                tempBild.URL = tempProfile.URL;
                tempBild.UserID = item.UserID;

                Model.BildLista.Add(tempBild);
            }

            return View(Model);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        //Här är vår inloggningsmetod.
        [HttpPost]
        public ActionResult LogIn(Login användare)
        {
            DBRep TempDB = new DBRep();
            var anv = TempDB.getUserFromString(användare.UName);
            if (anv != null && anv.PW == användare.PW)
            {
                Session["UserID"] = anv.UserID.ToString();
                Session["UName"] = anv.UName.ToString();
                CookieHandler cookieHandler = new CookieHandler();
                cookieHandler.CreateCookie(anv.UserID, this.ControllerContext);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Du har angett fel användarnamn eller lösenord");
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        //Här är httppost metoden för att registrera sig. 
        [HttpPost]
        public ActionResult Register(Registrera newAnv)
        {
            if (ModelState.IsValid)
            {
                Användare tempAnv = new Användare();
                tempAnv.Email = newAnv.Email;
                tempAnv.PW = newAnv.Lösenord;
                tempAnv.UName = newAnv.Användarnamn;

                DBRep OurDB = new DBRep();
                if (!OurDB.ledigtAnvNamn(newAnv.Användarnamn))
                {
                    ModelState.AddModelError("", "Tyvärr är detta användarnamn upptaget, var god gå tillbaka och välj en annan");
                    return Content("Tyvärr är detta användarnamn upptaget, var god gå tillbaka och välj en annan");
                }

                if (!OurDB.ledigEmail(newAnv.Email))
                {
                    ModelState.AddModelError("", "Tyvärr är denna email upptagen, var god gå tillbaka och välj en annan");
                    return Content("Tyvärr är denna email upptagen, var god gå tillbaka och välj en annan");
                }
                    OurDB.NewUser(tempAnv);

                ModelState.Clear();
                ViewBag.Message = "Välkommen " + tempAnv.UName + ", du är nu registrerad!";
            }

            return View();
        }

        //Metoden för att logga ut. 
        public ActionResult LoggaUt()
        {
            CookieHandler kakHanterare = new CookieHandler();
            kakHanterare.RemoveCookie(this.ControllerContext);
            return View();
        }
    }
}