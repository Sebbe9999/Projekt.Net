using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace Projekt.Net
{
    public class CookieHandler
    {
        public void CreateCookie(int ID, ControllerContext TheController)
        {

            HttpCookie cookie = new HttpCookie("BortaMatchCookie");
            cookie.Value = ID.ToString();

            TheController.HttpContext.Response.Cookies.Add(cookie);
        }

        public void RemoveCookie(ControllerContext TheController)
        {

            if (TheController.HttpContext.Request.Cookies.AllKeys.Contains("BortaMatchCookie"))
            {
                HttpCookie cookie = TheController.HttpContext.Request.Cookies["BortaMatchCookie"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                TheController.HttpContext.Response.Cookies.Add(cookie);
            }
        }
    }
}