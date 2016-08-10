using Projekt.Net.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Net.Controllers
{
    public class ProfilController : Controller
    {

        //Med denna metod så visas profilsidan för andra användare man t.ex. sökt fram genom sökfunktionen.
        // GET: Profil
        public ActionResult Index(int id)
        {
            DBRep DB = new DBRep();
            Profil Model = new Profil();

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

            var profil = DB.getProfile(id);
            var userFromDB = DB.getUser(id);
            Model.InläggsLista = DB.getInlägg(id);

            foreach(var item in Model.InläggsLista)
            {
                Model.Inläggsprofiler.Add(DB.getProfile(item.SID));
            }

            Model.FNamn = profil.FNamn;
            Model.ENamn = profil.ENamn;
            Model.Ålder = Convert.ToInt32(profil.Ålder);
            Model.Kön = Convert.ToInt32(profil.Kön);
            Model.Intressen = profil.Intresse;
            Model.URL = profil.URL;
            Model.UserID = profil.UID;

            Model.isFriends = false;
            Model.hasRequest = false;

            List<Friend> VänLista = DB.getFriends(Model.HeaderModel.UserID);
            foreach(var item in VänLista)
            {
                if(item.FirstUID == id || item.SecondUID == id)
                {
                    Model.isFriends = true;
                }
            }

            List<FriendRequest> RequestLista = DB.getAllFriendRequests(Model.HeaderModel.UserID);
            foreach(var item in RequestLista)
            {
                if(item.MUID == id || item.SUID == id)
                {
                    Model.hasRequest = true;
                }
            }

            if (!Model.HeaderModel.Inloggad)
            {
                return RedirectToAction("LogIn", "Start");
            }

            return View(Model);
        }

        //Här kan vi lägga till vänförfrågningar
        [HttpPost]
        public ActionResult AddFriend(Profil data)
        {
            DBRep db = new DBRep();
            FriendRequest temp = new FriendRequest();
            temp.SUID = data.SUID;
            temp.MUID = data.MUID;

            db.newFriendRequest(temp);
            return RedirectToAction("Index", new { id = data.MUID });
        }
    }
}