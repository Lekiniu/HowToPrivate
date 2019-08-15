using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using HowToWebApplication.Helpers;
using static HowToWebApplication.Helpers.PasswordHelper;
using HowToWebApplication.Models;


namespace HowToWebApplication.Filters
{
    public class AdminFilter : ActionFilterAttribute, IActionFilter
    {       
        public enum categories
        {
            admin = 1,
            user = 2
        }
         public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HowToDbEntities _db = new HowToDbEntities();
            users user = LoginHelper.CurrentUser();

            if (!LoginHelper.IsLoggedIn() || user.categoriesId != (int)categories.admin)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
            }
            else
            { var userPassword = SHA.GenerateSHA512String(user.password);
                var userFromDb = _db.users.FirstOrDefault(e => e.password == userPassword && e.email == user.email);
                if (userFromDb == null)
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            base.OnActionExecuting(filterContext);
        }
     }    
   }



   