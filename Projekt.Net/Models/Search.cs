using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class Search
    {
        public List<Användare> UserListan { get; set; }
        public string SökOrd { get; set; }
        public List<ProfilInfo> URLListan { get; set; }
        public Header HeaderModel { get; set; }

        public Search()
        {
            UserListan = new List<Användare>();
            URLListan = new List<ProfilInfo>();
            HeaderModel = new Header();
        }
    }
}