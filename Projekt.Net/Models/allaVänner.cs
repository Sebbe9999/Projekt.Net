using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class allaVänner
    {
        public List<ProfilInfo> VännerListan { get; set; }
        public int UserID { get; set; }
        public Header HeaderModel { get; set; }

        public allaVänner()
        {
            HeaderModel = new Header();
            VännerListan = new List<ProfilInfo>();
        }
    }
}