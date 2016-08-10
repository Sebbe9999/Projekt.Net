using Projekt.Net.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projekt.Net.Controllers
{
    public class BortaMatchController : ApiController
    {
        /**
        Denna metod är till för att göra ett nytt inlägg på någons "wall". 
        Det är ett API anrop.
                   
            */
        public void nyttInlägg(nyttInlägg data)
        {
            DBRep DB = new DBRep();
            if(data.Post == null)
            {
                data.Post = "Jag skrev inget i fältet, så det blir detta felmeddelande istället.";
            }
            DB.läggTillPost(data.Sender, data.Receiver, data.Post);
        }
    }
}
