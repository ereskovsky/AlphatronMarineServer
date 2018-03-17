using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlphatronMarineServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Auth", action = "Login"}
            );
            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Auth", action = "Logout" }
            );
            routes.MapRoute(
                name: "Auth",
                url: "Authorize",
                defaults: new { controller = "Auth", action = "Authorize" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
            routes.MapRoute(
                name: "VesselTemplate",
                url: "Vessel/{id}",
                defaults: new { controller = "CRUD", action = "VesselTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "VesselDelete",
                url: "Vessel/{id}/del",
                defaults: new { controller = "CRUD", action = "VesselDelete", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserTemplate",
                url: "User/{id}",
                defaults: new { controller = "CRUD", action = "UserTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserDelete",
                url: "User/{id}/del",
                defaults: new { controller = "CRUD", action = "UserDelete", id = UrlParameter.Optional }
            );
        }
    }
}
