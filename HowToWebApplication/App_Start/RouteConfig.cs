using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HowToWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" },   // en or en-US
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
);


            routes.MapRoute(
                name: "second",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "ArticlesList", id = UrlParameter.Optional }
            );
        }
    }
}
