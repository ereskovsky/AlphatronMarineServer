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
            if (auth.CheckAuthStatus(uid, utoken))
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