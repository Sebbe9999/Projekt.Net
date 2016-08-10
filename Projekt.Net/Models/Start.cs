using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class Start
    {
        public List<RandomBild> BildLista { get; set; }
        public Header HeaderModel { get; set; }
        public int MyProperty { get; set; }

        public Start()
        {
            BildLista = new List<RandomBild>();
            HeaderModel = new Header();
        }
    }
}