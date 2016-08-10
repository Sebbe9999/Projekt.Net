using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class FRequest
    {
        public List<FriendRequest> RequestLista { get; set; }
        public List<ProfilInfo> ProfilLista { get; set; }
        public List<Användare> AnvändareLista { get; set; }
        public bool bliVän { get; set; }
        public int UserID { get; set; }
        public int FriendID { get; set; }
        public Header HeaderModel { get; set; }

        public FRequest()
        {
            RequestLista = new List<FriendRequest>();
            ProfilLista = new List<ProfilInfo>();
            AnvändareLista = new List<Användare>();
            HeaderModel = new Header();
        }

    }
}