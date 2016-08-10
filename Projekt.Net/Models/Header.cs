using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class Header
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public bool Inloggad { get; set; }
        public List<FriendRequest> FriendRequestList { get; set; }

        public Header()
        {
            FriendRequestList = new List<FriendRequest>();
            Inloggad = false;
        }
    }
}