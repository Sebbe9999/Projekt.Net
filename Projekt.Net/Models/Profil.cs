using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class Profil
    {
        public int UserID { get; set; }
        public string FNamn { get; set; }
        public string ENamn { get; set; }
        public int Ålder { get; set; }
        public int Kön { get; set; }
        public string Intressen { get; set; }
        public string URL { get; set; }
        public int MUID { get; set; }
        public int SUID { get; set; }
        public bool isFriends { get; set; }
        public bool hasRequest { get; set; }
        public Header HeaderModel { get; set; }
        public List<Wall> InläggsLista { get; set; }
        public List<ProfilInfo> Inläggsprofiler { get; set; }

        public Profil()
        {
            HeaderModel = new Header();
            InläggsLista = new List<Wall>();
            Inläggsprofiler = new List<ProfilInfo>();
        }
    }
}