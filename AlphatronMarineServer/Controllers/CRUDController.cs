using AlphatronMarineServer.Models;
using Newtonsoft.Json;
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
                ViewBag.VID = id;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
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
                ViewBag.Users = db.User;
                ViewBag.Access = db.VesselAccess;
                if (id == 0)
                {
                    db.Vessel.Add(v);
                    //db.SaveChanges();
                    //if (Request.Form["VesselAccess"] != null)
                    //{
                    //    var rt = (Request.Form["VesselAccess"]).Split(',');

                    //    foreach (var item in rt)
                    //    {
                    //        var tags = db.VesselAccess.Where(v => v.VesselIMO == v.IMO);
                    //        db.ArticleJoinTag.RemoveRange(tags);
                    //        db.ArticleJoinTag.Add(new ArticleJoinTag { ArticleID = a.ID, TagID = int.Parse(@item) });
                    //    }
                    //}
                }
                else
                {
                    //db.Vessel.Find(id).IMO = v.IMO;
                    db.Vessel.Find(id).Name = v.Name;
                    db.Vessel.Find(id).Type = v.Type;
                    db.Vessel.Find(id).AnnualCheckDate = v.AnnualCheckDate;
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
                    db.Vessel.Remove(db.Vessel.Find(id));
                    db.SaveChanges();

                    return Redirect("~/Fleet");
                }
                else
                    return Redirect("~/Login");
        }
        [HttpGet]
        public ActionResult EquipmentTemplate(int id, int imo)
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

                ViewBag.VIMO = imo;
                ViewBag.EquipmentTemplates = db.EquipmentTemplates;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.EID = id;
                ViewBag.Vessels = db.Vessel;
                ViewBag.Part = "Equipment";
                if (id == 0)
                {

                    return View(new Equipment { });
                }
                else
                {
                    Equipment eq = db.Equipment.Find(id);
                    return View(eq);
                }
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult EquipmentTemplate(int id, int imo, Equipment eq)
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
                ViewBag.VIMO = imo;
                ViewBag.EquipmentTemplates = db.EquipmentTemplates;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.EID = id;
                if (id == 0)
                {
                    eq.VesselIMO = imo;
                    db.Equipment.Add(eq);
                }
                else
                {
                    db.Equipment.Find(id).Model = eq.Model;
                    db.Equipment.Find(id).Name = eq.Name;
                    db.Equipment.Find(id).CheckDate = eq.CheckDate;
                    db.Equipment.Find(id).Maker = eq.Maker;
                    db.Equipment.Find(id).Remarks = eq.Remarks;
                    db.Equipment.Find(id).VesselIMO = imo;

                };
                db.SaveChanges();
                return Redirect("~/Vessel/" + imo+ "/Equipment");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult EquipmentDelete(int id)
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
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken))
            {
                db.Equipment.Remove(db.Equipment.Find(id));
                db.SaveChanges();

                return Redirect("~/Equipment");
            }
            else
                return Redirect("~/Login");
        }
        [HttpGet]
        public ActionResult UserTemplate(int id)
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
                ViewBag.UID = id;
                ViewBag.Roles = db.Roles;
                ViewBag.Companies = db.Company.OrderBy(a => a.Name);
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
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                if (auth.CheckAuthStatus(uid, utoken))
                {
                    db.User.Remove(db.User.Find(id));
                    db.SaveChanges();

                    return Redirect("~/Users");
                    }
                 else
                return Redirect("~/Login");
        }


        [HttpGet]
        public ActionResult ETTemplate(int id)
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
                ViewBag.ETID = id;
                ViewBag.Part = "Equipment";
                if (id == 0)
                {

                    return View(new EquipmentTemplates { });
                }
                else
                {
                    EquipmentTemplates et = db.EquipmentTemplates.Find(id);
                    return View(et);
                }
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult ETTemplate(int id, EquipmentTemplates et)
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
                if (id == 0)
                {
                    List<EqField> listFields = new List<EqField>();
                    foreach (var item in et.Fields.Split(';'))
                    {
                        listFields.Add(new EqField { Name = item });
                    }
                    et.Fields = JsonConvert.SerializeObject(listFields);
                    db.EquipmentTemplates.Add(et);
                }
                else
                {
                    List<EqField> listFields = new List<EqField>();
                    foreach (var item in et.Fields.Split(';'))
                    {
                        listFields.Add(new EqField { Name = item });
                    }
                    db.EquipmentTemplates.Find(id).Name = et.Name;
                    db.EquipmentTemplates.Find(id).Fields = JsonConvert.SerializeObject(listFields);
                }
                db.SaveChanges();
                return Redirect("~/Users");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult ETDelete(int id)
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
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken))
            {
                db.User.Remove(db.User.Find(id));
                db.SaveChanges();

                return Redirect("~/Users");
            }
            else
                return Redirect("~/Login");
        }

        [HttpGet]
        public ActionResult ETFields(int id, int imo)
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

                ViewBag.VIMO = imo;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Equipment";
                Equipment e = db.Equipment.Find(id);
                ViewBag.E = e;
               return View();
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult ETFieldsSave(int id, int imo)
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
                Dictionary<string, string> dic = new Dictionary<string, string>();
                 foreach (string item in Request.Form.Keys)
                 {
                    dic.Add(item, Request.Form[item]);
                 }
                string encoded = JsonConvert.SerializeObject(dic);
                db.Equipment.Find(id).Fields = encoded;
                db.SaveChanges();
                return Redirect("~/Vessel/"+imo+"/Equipment");
            }
            else
                return Redirect("~/Login");

        }

    }
}