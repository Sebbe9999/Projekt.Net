using Projekt.Net.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Net.Controllers
{
    //Denna metod är till för att visa all vännerförfrågningar en person har.
    public class FriendRequestController : Controller
    {
        // GET: FriendRequest
        public ActionResult Index(int id)
        {
            DBRep DB = new DBRep();
            FRequest Model = new FRequest();

            if (!DB.korrektID(id))
            {
                return RedirectToAction("LogIn", "Start");
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

            Model.RequestLista = DB.getFriendRequests(id);
            Model.UserID = id;
            
            foreach(var item in Model.RequestLista)
            {
                Model.ProfilLista.Add(DB.getProfile(item.SUID));
                Model.AnvändareLista.Add(DB.getUser(item.SUID));
            }

            return View(Model);
        }

        //Här hanterar vi deras svar på en förfrågning. Antingen så blir det ja eller nej.
        //Ja = Ta bort vännerförfrågan samt lägga till i vännerlistan. 
        //Nej = Ta bort vännerförfrågan endast.
        [HttpPost]
        public ActionResult HanteraRequest(FRequest data)
        {
            Friend newFriend = new Friend();
            DBRep DB = new DBRep();
            
            if (data.bliVän == true)
            {
                newFriend.FirstUID = data.UserID;
                newFriend.SecondUID = data.FriendID;

                DB.newFriend(newFriend);
                DB.deleteFriendRequest(data.FriendID, data.UserID);
            }
            else
            {
                DB.deleteFriendRequest(data.FriendID, data.UserID);
            }
            return RedirectToAction("Index", new { id = data.UserID });
        }
    }
}