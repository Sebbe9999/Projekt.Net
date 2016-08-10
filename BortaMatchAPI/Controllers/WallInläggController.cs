using BortaMatchAPI.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BortaMatchAPI.Controllers
{
    public class WallInläggController : ApiController
    {
        [HttpPost]
        public void postaInlägg(FormCollection data)
        {
            DBRep DB = new DBRep();
            DB.läggTillPost(data.SenderID, data.MottagarID, data.Post);
        }
    }
}
