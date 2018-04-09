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
            //API
            routes.MapRoute(
                name: "APIVessels",
                url: "API/{action}/{id}",
                defaults: new { controller = "API", action = "Vessels", id = UrlParameter.Optional }
            );
            //Auth
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

            //RoteTemplate
            routes.MapRoute(
                name: "Default",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index"}
            );

            //Vessels
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

            //Equipment
            routes.MapRoute(
                name: "EquipTemplate",
                url: "Equipment/{id}",
                defaults: new { controller = "CRUD", action = "EquipmentTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EquipDelete",
                url: "Equipment/{id}/del",
                defaults: new { controller = "CRUD", action = "EquipmentDelete", id = UrlParameter.Optional }
            );

            //Companies
            routes.MapRoute(
                name: "CompanyTemplate",
                url: "Company/{id}",
                defaults: new { controller = "CRUD", action = "CompanyTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CompanyDelete",
                url: "Company/{id}/del",
                defaults: new { controller = "CRUD", action = "CompanyDelete", id = UrlParameter.Optional }
            );

            //Users
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
