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
            HttpCookie cookie = Request.Cookies["User"];
            if (auth.CheckAuthStatus(int.Parse(cookie["id"]), cookie["token"]))
            {
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
            HttpCookie cookie = Request.Cookies["User"];
            
            if (auth.CheckAuthStatus(int.Parse(cookie["id"]), cookie["token"]))
            {
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
                HttpCookie cookie = Request.Cookies["User"];
                
                if (auth.CheckAuthStatus(int.Parse(cookie["id"]), cookie["token"]))
                {
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Users";
                    ViewBag.Users = db.User;
                    return View();
                    }
                else
                    return Redirect("~/Login");
        }
        public ActionResult Equipment()
        {
            HttpCookie cookie = Request.Cookies["User"];

            if (auth.CheckAuthStatus(int.Parse(cookie["id"]), cookie["token"]))
            {
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Equipment";
                ViewBag.Equipment = db.Equipment;
                return View();
            }
            else
                return Redirect("~/Login");
        }

    }
}