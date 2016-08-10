using Projekt.Net.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Net.Controllers
{
    public class MinProfilController : Controller
    {
        //Här visas den inloggade personens profil, shit hämtas från DB o skickas till modellen.
        //Sen visas viewen. 
        // GET: MinProfil
        public ActionResult Index(int id)
        {

            DBRep DB = new DBRep();
            MinProfil Model = new MinProfil();

            if (!DB.korrektID(id))
            {
                return RedirectToAction("Index", "Start");
            }

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

            var tempProfil = DB.getProfile(id);
            var tempUser = DB.getUser(id);

            Model.Kön = Convert.ToInt32(tempProfil.Kön);
            Model.Intresse = tempProfil.Intresse;
            Model.FNamn = tempProfil.FNamn;
            Model.ENamn = tempProfil.ENamn;
            Model.UserID = id;
            Model.Ålder = Convert.ToInt32(tempProfil.Ålder);
            Model.URL = tempProfil.URL;

            Model.InläggsLista = DB.getInlägg(id);

            var tempFriends = DB.getFriends(id);

            foreach (var item in tempFriends)
            {
                if (item.FirstUID == id)
                {
                    Model.VännerLista.Add(DB.getUser(item.SecondUID));
                }
                else if (item.SecondUID == id)
                {
                    Model.VännerLista.Add(DB.getUser(item.FirstUID));
                }
            }

            foreach (var item in Model.VännerLista)
            {
                Model.VännerProfiler.Add(DB.getProfile(item.UserID));
            }

            foreach (var item in Model.InläggsLista)
            {
                Model.InläggsProfiler.Add(DB.getProfile(item.SID));
            }

            Model.RandomLista = DB.getRandomFriendProfiles(id);

            return View(Model);
        }

        //Här kan vi ta bort ett inlägg från våran wall. 
        [HttpPost]
        public ActionResult taBortInlägg(MinProfil data)
        {
            DBRep DB = new DBRep();
            DB.taBortInlägg(data.WIDtoRemove);

            return RedirectToAction("Index", new { id = data.UserID});
        }

        //När denna körs så kommer alla vänner som tillhör den inloggade personen.
        public ActionResult allaVänner(int id)
        {
            allaVänner Model = new Models.allaVänner();
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

            if(!Model.HeaderModel.Inloggad)
            {
                return RedirectToAction("LogIn", "Start");
            }

            var tempFriends = DB.getFriends(id);

            foreach(var item in tempFriends)
            {
                if (item.FirstUID != 0 || item.SecondUID != 0)
                {
                    if (item.FirstUID == id)
                    {
                        var temp = DB.getProfile(item.SecondUID);
                        Model.VännerListan.Add(temp);
                    }
                    else
                    {
                        var temp = DB.getProfile(item.FirstUID);
                        Model.VännerListan.Add(temp);
                    }
                }
            }

            Model.UserID = Model.HeaderModel.UserID;

            return View(Model);
        }

        //Här är controllermetoden som skickar en view där vi kan ändra våra 
        //uppgifter.
        public ActionResult visaRedigeraProfil(int id)
        {
            DBRep DB = new DBRep();
            RedigeraProfil Model = new RedigeraProfil();

            if (!DB.korrektID(id))
            {
                return RedirectToAction("Index", "Start");
            }

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

            if(id != Model.HeaderModel.UserID)
            {
                return RedirectToAction("Index", "Start");
            }

            Model.Användaren = DB.getUser(id);
            Model.Profilen = DB.getProfile(id);
            Model.UserID = Model.Användaren.UserID;

            return View(Model);
        }

        [HttpPost]
        public ActionResult sparaRedigeraProfil(RedigeraProfil data)
        {
            if (data.Profilen.FNamn == null || data.Profilen.ENamn == null || data.Profilen.Intresse == null || data.Användaren.Email == null || data.Användaren.UName == null || data.Användaren.PW == null || data.Profilen.URL == null)
            {
                ModelState.AddModelError("", "Du har något/några tomma fält, var god gå tillbaka och ordna detta!");
                return Content("Du har något/några tomma fält, var god gå tillbaka och ordna detta!");
            }
            DBRep DB = new DBRep();

            if (!data.oldUname.Equals(data.Användaren.UName))
            {
                if (!DB.ledigtAnvNamn(data.Användaren.UName))
                {
                    ModelState.AddModelError("", "Tyvärr är användarnamnet taget, var god välj ett annat");
                    return Content("Tyvärr är användarnamnet taget, var god välj ett annat");
                }
            }

            Regex regex = new Regex(@"(?:([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:jpg|gif|png))(?:\?([^#]*))?(?:#(.*))?");
            Match match = regex.Match(data.Profilen.URL);
            if (!match.Success)
            {
                ModelState.AddModelError("", "Bilden är inte giltig, var god välj en annan");
                return Content("Bilden är inte giltig, var god välj en annan");
            }

            if (!data.oldEmail.Equals(data.Användaren.Email))
            {
                if (!DB.ledigEmail(data.Användaren.Email))
                {
                    ModelState.AddModelError("", "Tyvärr är denna email upptagen, var god gå tillbaka och välj en annan");
                    return Content("Tyvärr är denna email upptagen, var god gå tillbaka och välj en annan");
                }
            }

            DB.uppdateraAnvändaren(data.Användaren, data.Profilen);
            return RedirectToAction("Index", new { id = data.Användaren.UserID });
        }
    }
}