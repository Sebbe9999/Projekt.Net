using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class RedigeraProfil
    {
        public Header HeaderModel { get; set; }
        public ProfilInfo Profilen { get; set; }
        public Användare Användaren { get; set; }
        public int UserID { get; set; }

        public string oldUname { get; set; }
        public string oldEmail { get; set; }

        public RedigeraProfil()
        {
            HeaderModel = new Header();
        }
    }

}