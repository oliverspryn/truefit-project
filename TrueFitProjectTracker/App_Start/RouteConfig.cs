using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrueFitProjectTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //Project
            routes.MapRoute(
                "Project",
                "project/{id}/{name}",
                new { controller = "Project", action = "Index", id = UrlParameter.Optional },
                new { id = @"\d+" }
            );

        //Home page
            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "ProjectsList", id = UrlParameter.Optional }
            );
        }
    }
}