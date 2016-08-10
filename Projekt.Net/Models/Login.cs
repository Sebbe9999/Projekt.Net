using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class Login
    {
        public int UserID { get; set; }
        public string UName { get; set; }
        public string Email { get; set; }
        public string PW { get; set; }
    }
}