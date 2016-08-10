using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BortaMatchAPI.Models
{
    public class WallInlägg
    {
        public int SenderID { get; set; }
        public int MottagarID { get; set; }
        public string Post { get; set; }
    }
}