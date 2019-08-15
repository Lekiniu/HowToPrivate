using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HowToWebApplication.Models;

namespace HowToWebApplication.Helpers
{
    public class LoginHelper
    {
        public static void LogOff()
        {
            HttpContext.Current.Session["user"] = null;
        }

        public static users CurrentUser()
        {
            return (users)HttpContext.Current.Session["user"];
        }

        public static bool IsLoggedIn()
        {
            var result = (users)HttpContext.Current.Session["user"] != null;
            return result;
        }

        public static void CreateUser(users user)
        {
            HttpContext.Current.Session["user"] = user;
            //HttpContext.Current.Session.Timeout = 1;
        }
    }
}