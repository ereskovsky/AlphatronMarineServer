using AlphatronMarineServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlphatronMarineServer.Controllers
{
    public class CRUDController : Controller
    {
        AlphatronMarineEntities db = new AlphatronMarineEntities();
        AuthController auth = new AuthController();
        [HttpGet]
        public ActionResult VesselTemplate(int id)
        {
            HttpCookie cookie = Request.Cookies["User"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(cookie))
            {
                ViewBag.Companies = db.Company.OrderBy(a=>a.Name);
            ViewBag.Countries = db.Country.OrderBy(a => a.Name);
            ViewBag.Part = "Fleet";
            if (id == 0)
            {

                return View(new Vessel { });
            }
            else
            {
                Vessel v = db.Vessel.Find(id);
                return View(v);
            }
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult VesselTemplate(int id, Vessel v)
        {
            HttpCookie cookie = Request.Cookies["User"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(cookie))
            {
                ViewBag.Part = "Fleet";
            if (id == 0)
            {
                db.Vessel.Add(v);
            }
            else
            {
                db.Vessel.Find(id).IMO = v.IMO;
                db.Vessel.Find(id).Name = v.Name;
                db.Vessel.Find(id).Type = v.Type;
                db.Vessel.Find(id).MMSI = v.MMSI;
                db.Vessel.Find(id).CallSign = v.CallSign;
                db.Vessel.Find(id).GrossTonnage = v.GrossTonnage;
                db.Vessel.Find(id).Flag = v.Flag;
                db.Vessel.Find(id).CompanyID = v.CompanyID;
            }
            db.SaveChanges();
            return Redirect("~/Fleet");
            }
            else
                return Redirect("~/Login");


        }
        public ActionResult VesselDelete(int id)
        {
                HttpCookie cookie = Request.Cookies["User"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                if (auth.CheckAuthStatus(cookie))
                {
                    db.Vessel.Remove(db.Vessel.Find(id));
            db.SaveChanges();

            return Redirect("~/Fleet");
            }
            else
                return Redirect("~/Login");
        }
        [HttpGet]
        public ActionResult UserTemplate(int id)
        {
            HttpCookie cookie = Request.Cookies["User"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(cookie))
            {
                ViewBag.UID = id;
            ViewBag.Roles = db.Roles;
            ViewBag.Companies = db.Company.OrderBy(a => a.Name);
            ViewBag.UID = id;
            ViewBag.Part = "Users";
            if (id == 0)
            {

                return View(new User{ });
            }
            else
            {
                User a = db.User.Find(id);
                return View(a);
            }
            }
            else
                return Redirect("~/Login");
        }
       
        [HttpPost]
        public ActionResult UserTemplate(int id, User u)
        {
            HttpCookie cookie = Request.Cookies["User"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(cookie))
            {

                if (id == 0)
            {
                u.Password = MD5Hasher.Hash(u.Password);
                db.User.Add(u);
            }
            else
            {
                db.User.Find(id).Name = u.Name;
                db.User.Find(id).Email = u.Email;
                db.User.Find(id).RoleID = u.RoleID;
                db.User.Find(id).CompanyID = u.CompanyID;
            }
            db.SaveChanges();
            return Redirect("~/Users");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult UserDelete(int id)
        {
                HttpCookie cookie = Request.Cookies["User"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                if (auth.CheckAuthStatus(cookie))
                {
                    db.User.Remove(db.User.Find(id));
                    db.SaveChanges();

                    return Redirect("~/Users");
                    }
                 else
                return Redirect("~/Login");
        }

    }
}