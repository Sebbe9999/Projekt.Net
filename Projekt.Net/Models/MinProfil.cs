using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class MinProfil
    {
        public string URL { get; set; }
        public int Ålder { get; set; }
        public int Kön { get; set; }
        public string Intresse { get; set; }
        public string  ENamn { get; set; }
        public string FNamn { get; set; }
        public int UserID { get; set; }
        public int WIDtoRemove { get; set; }
        public List<ProfilInfo> RandomLista { get; set; }
        public List<Wall> InläggsLista { get; set; }
        public List<Användare> VännerLista { get; set; }
        public List<ProfilInfo> VännerProfiler { get; set; }
        public List<ProfilInfo> InläggsProfiler { get; set; }
        public Header HeaderModel { get; set; }

        public MinProfil()
        {
            InläggsLista = new List<Wall>();
            HeaderModel = new Header();
            VännerLista = new List<Användare>();
            VännerProfiler = new List<ProfilInfo>();
            InläggsProfiler = new List<ProfilInfo>();
            RandomLista = new List<ProfilInfo>();
        }
    }
}