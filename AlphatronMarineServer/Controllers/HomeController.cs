using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphatronMarineServer.Models;

namespace AlphatronMarineServer.Controllers
{
    public class HomeController : Controller
    {

        AuthController auth = new AuthController();
        AlphatronMarineEntities db = new AlphatronMarineEntities();
        public ActionResult Index()
        {
            
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null){
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else{
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken))
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Main";
                return View();
            }
            else
                return Redirect("~/Login");
        }

        public ActionResult Fleet()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken))
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Fleet";
                ViewBag.Vessels = db.Vessel;
                return View();
            }
            else
                return Redirect("~/Login");
        }
        public ActionResult Users()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
                {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Users";
                    ViewBag.Users = db.User;
                    return View();
                    }
                else
                    return Redirect("~/Login");
        }
        public ActionResult Equipment(int imo)
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken))
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Equipment";
                ViewBag.VIMO = imo;
                ViewBag.Equipment = db.Equipment.Where(x=>x.VesselIMO == imo);
                return View();
            }
            else
                return Redirect("~/Login");
        }
        public ActionResult EquipmentTemplates()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Equipment";
                ViewBag.EquipmentTemplates = db.EquipmentTemplates;
                return View();
            }
            else
                return Redirect("~/Login");
        }

        public ActionResult Changes()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Changes";
                ViewBag.Temp = db.Temp;
                return View();
            }
            else
                return Redirect("~/Login");
        }
        public ActionResult Products()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.db = db;
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Products";
                ViewBag.Products = db.Product;
                return View();
            }
            else
                return Redirect("~/Login");
        }

        public ActionResult PBFacts(int id)
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Products";
                ViewBag.PID = id;
                ViewBag.Facts = db.ProductBulletFact.Where(x=>x.ProductID == id);
                return View();
            }
            else
                return Redirect("~/Login");
        }
        public ActionResult Locations()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Locations";
                ViewBag.Locations = db.BusinessLocation;
                return View();
            }
            else
                return Redirect("~/Login");
        }
    }
}