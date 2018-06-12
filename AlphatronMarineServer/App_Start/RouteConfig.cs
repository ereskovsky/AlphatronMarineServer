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
                url: "API/{action}",
                defaults: new { controller = "API", action = "UsersVessels" }
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
            routes.MapRoute(
                name: "StatusCheck",
                url: "Auth/CheckAuthStatus",
                defaults: new { controller = "Auth", action = "CheckAuthStatus" }
            );
            routes.MapRoute(
                name: "AcceptVesselChanges",
                url: "Change/Accept/Vessel/{id}",
                defaults: new { controller = "CRUD", action = "AcceptVessel", id = UrlParameter.Optional }
            );
            //RouteTemplate
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
               name: "Equipment",
               url: "Vessel/{imo}/Equipment",
               defaults: new { controller = "Home", action = "Equipment", imo = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "EquipTemplate",
                url: "Vessel/{imo}/Equipment/{id}",
                defaults: new { controller = "CRUD", action = "EquipmentTemplate", id = UrlParameter.Optional, imo = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EquipDelete",
                url: "Vessel/{id}/Equipment/{eid}/del",
                defaults: new { controller = "CRUD", action = "EquipmentDelete", id = UrlParameter.Optional,  eid = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AcceptEquipmentChanges",
                url: "Change/Accept/Equipment/{id}",
                defaults: new { controller = "CRUD", action = "AcceptEquip", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ETFields",
                url: "Vessel/{imo}/Equipment/{id}/Fields",
                defaults: new { controller = "CRUD", action = "ETFields", id = UrlParameter.Optional,  imo = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ETFieldsSave",
                url: "Vessel/{imo}/Equipment/{id}/ETFieldsSave",
                defaults: new { controller = "CRUD", action = "ETFieldsSave", id = UrlParameter.Optional, imo = UrlParameter.Optional }
            );


            //EquipmentTemplates
            routes.MapRoute(
                name: "ETTemplate",
                url: "ETTemplate/{id}",
                defaults: new { controller = "CRUD", action = "ETTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ETDelete",
                url: "ETDelete/{id}/del",
                defaults: new { controller = "CRUD", action = "ETDelete", id = UrlParameter.Optional }
            );
            //Changes
            routes.MapRoute(
                name: "DeclineChange",
                url: "Change/Decline/{id}/del",
                defaults: new { controller = "CRUD", action = "ETDelete", id = UrlParameter.Optional }
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
            routes.MapRoute(
                name: "AssignedVessel",
                url: "Users/{id}/Vessels",
                defaults: new { controller = "Home", action = "AssignedVessels", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AssignVessel",
                url: "User/{id}/Vessels/Assign",
                defaults: new { controller = "CRUD", action = "AssignVessel", id = UrlParameter.Optional, imo = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UnassignVessel",
                url: "User/{id}/Vessels/{imo}/Unassign",
                defaults: new { controller = "CRUD", action = "UnassignVessel", id = UrlParameter.Optional, imo = UrlParameter.Optional }
            );


            //Products
            routes.MapRoute(
                name: "ProductTemplate",
                url: "Product/{id}",
                defaults: new { controller = "CRUD", action = "ProductTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductDelete",
                url: "Product/{id}/del",
                defaults: new { controller = "CRUD", action = "ProductDelete", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PBFacts",
                url: "Product/{id}/Facts",
                defaults: new { controller = "Home", action = "PBFacts", id = UrlParameter.Optional }
            );

            //PBFacts
            routes.MapRoute(
                name: "PBFactTemplate",
                url: "Product/{id}/Fact/{fact_id}",
                defaults: new { controller = "CRUD", action = "PBFactTemplate", id = UrlParameter.Optional, fact_id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PBFactDelete",
                url: "Product/{id}/Fact/{fact_id}/del",
                defaults: new { controller = "CRUD", action = "PBFactDelete", id = UrlParameter.Optional, fact_id = UrlParameter.Optional }
            );

            //Locations
            routes.MapRoute(
                name: "LocationsTemplate",
                url: "Location/{id}",
                defaults: new { controller = "CRUD", action = "LocationTemplate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LocationsDelete",
                url: "Location/{id}/del",
                defaults: new { controller = "CRUD", action = "LocationDelete", id = UrlParameter.Optional }
            );


        }
    }
}
