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
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
        public ActionResult VesselTemplate(int id, Vessel v, HttpPostedFileBase Picture = null)
        {
            string fullPath = null;

            if (Picture != null)
            {
                var path = System.IO.Path.GetFileName(Picture.FileName);
                fullPath = "/Content/images/" + MD5Hasher.Hash(DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortDateString()) + path;
                fullPath = fullPath.Replace(" ", "_");
                Picture.SaveAs(Server.MapPath(fullPath));
            }
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
                ViewBag.Users = db.User;
                ViewBag.Access = db.VesselAccess;
                v.Picture = fullPath;
                db.Temp.Add(new Temp { Type = "Vessel", ObjectID = v.IMO, SerializedObject = JsonConvert.SerializeObject(v), Date = DateTime.Now });
                db.SaveChanges();
                return Redirect("~/Fleet");
            }
            else
                return Redirect("~/Login");


        }
        
        public ActionResult AcceptVessel(int id)
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
                Vessel v = db.Temp.Find(id).NewVessel;
                if (db.Vessel.Find(v.IMO) == null)
                {
                    db.Vessel.Add(v);
                    db.Temp.Remove(db.Temp.Find(id));
                }
                else
                {
                    db.Vessel.Find(v.IMO).Name = v.Name;
                    db.Vessel.Find(v.IMO).Type = v.Type;
                    db.Vessel.Find(v.IMO).AnnualCheckDate = v.AnnualCheckDate;
                    db.Vessel.Find(v.IMO).MMSI = v.MMSI;
                    db.Vessel.Find(v.IMO).CallSign = v.CallSign;
                    db.Vessel.Find(v.IMO).GrossTonnage = v.GrossTonnage;
                    db.Vessel.Find(v.IMO).Flag = v.Flag;
                    db.Vessel.Find(v.IMO).CompanyID = v.CompanyID;
                    db.Vessel.Find(v.IMO).Picture = v.Picture;
                    db.Temp.Remove(db.Temp.Find(id));
                };
                db.SaveChanges();
                return Redirect("~/Changes");
            }
            else
                return Redirect("~/Login");

        }

        public ActionResult DeclineChange(int id)
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
                db.Temp.Remove(db.Temp.Find(id));
                db.SaveChanges();
                return Redirect("~/Changes");
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
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.VIMO = imo;
                ViewBag.EquipmentTemplates = db.EquipmentTemplates;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.EID = id;
                eq.VesselIMO = imo;
                db.Temp.Add(new Temp { Type = "Equipment", ObjectID = eq.SerialNumber, SerializedObject = JsonConvert.SerializeObject(eq), Date = DateTime.Now });
                db.SaveChanges();
                return Redirect("~/Vessel/" + imo + "/Equipment");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult AcceptEquip(int id)
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
                Equipment eq = db.Temp.Find(id).NewEquipment;
                if (db.Equipment.Find(eq.SerialNumber) == null)
                {
                    db.Equipment.Add(eq);
                }
                else
                {
                    db.Equipment.Find(eq.SerialNumber).Model = eq.Model;
                    db.Equipment.Find(eq.SerialNumber).Name = eq.Name;
                    db.Equipment.Find(eq.SerialNumber).CheckDate = eq.CheckDate;
                    db.Equipment.Find(eq.SerialNumber).Maker = eq.Maker;
                    db.Equipment.Find(eq.SerialNumber).Remarks = eq.Remarks;
                    db.Equipment.Find(eq.SerialNumber).Fields = eq.Fields;
                    db.Temp.Remove(db.Temp.Find(id));

                };
                db.SaveChanges();
                return Redirect("~/Changes");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult EquipmentDelete(int id, int eid)
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
            ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken))
            {
                db.Equipment.Remove(db.Equipment.Find(eid));
                db.SaveChanges();

                return Redirect("~/Vessel/" + id + "/Equipment");
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

            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
            ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
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

            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
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
            ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
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
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.VIMO = imo;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.Part = "Vessel";
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
                ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                 foreach (string item in Request.Form.Keys)
                 {
                    dic.Add(item, Request.Form[item]);
                 }
                string encoded = JsonConvert.SerializeObject(dic);
                Equipment eq = db.Equipment.Find(id);
                eq.Fields = encoded;
                db.Temp.Add(new Temp { Type = "Equipment", ObjectID = eq.SerialNumber, SerializedObject = JsonConvert.SerializeObject(eq), Date = DateTime.Now });
                db.SaveChanges();
                return Redirect("~/Vessel/"+imo+"/Equipment");
            }
            else
                return Redirect("~/Login");

        }

        [HttpGet]
        public ActionResult ProductTemplate(int id)
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
                ViewBag.UID = id;
                ViewBag.Roles = db.Roles;
                ViewBag.Companies = db.Company.OrderBy(a => a.Name);
                ViewBag.Part = "Products";
                if (id == 0)
                {

                    return View(new Product { });
                }
                else
                {
                    Product p = db.Product.Find(id);
                    return View(p);
                }
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult ProductTemplate(int id, Product p, HttpPostedFileBase Picture = null, HttpPostedFileBase Manual = null)
        {
            string fullPicPath = null;
            string fullManPath = null;
            if (Picture != null)
            {
                var path = System.IO.Path.GetFileName(Picture.FileName);
                fullPicPath = "/Content/images/" + MD5Hasher.Hash(DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortDateString()) + path;
                fullPicPath = fullPicPath.Replace(" ", "_");
                Picture.SaveAs(Server.MapPath(fullPicPath));
            }
            if (Manual != null)
            {
                var path = System.IO.Path.GetFileName(Manual.FileName);
                fullManPath = "/Content/manuals/" + MD5Hasher.Hash(DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortDateString()) + path;
                fullManPath = fullManPath.Replace(" ", "_");
                Manual.SaveAs(Server.MapPath(fullManPath));
            }

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
                if (id == 0)
                {
                    
                    db.Product.Add(p);
                    db.SaveChanges();
                    ProductFiles pf = new ProductFiles { Manual = fullManPath, Picture = fullPicPath, ProductID = p.ID };
                    db.ProductFiles.Add(pf);
                    db.SaveChanges();
                    
                }
                else
                {
                    db.Product.Find(id).Name = p.Name;
                    db.Product.Find(id).ShortDescription = p.ShortDescription;
                    db.Product.Find(id).FullDescription = p.FullDescription;
                    if (Manual != null)
                    {
                        db.ProductFiles.Where(x => x.ProductID == p.ID).FirstOrDefault().Manual = fullManPath;
                    }
                    if (Picture != null)
                    {
                        db.ProductFiles.Where(x => x.ProductID == p.ID).FirstOrDefault().Picture = fullPicPath;
                    }
                    
                    db.SaveChanges();
                }
                
                return Redirect("~/Products");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult ProductDelete(int id)
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
            ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                db.Product.Remove(db.Product.Find(id));
                db.SaveChanges();

                return Redirect("~/Products");
            }
            else
                return Redirect("~/Login");
        }

        [HttpGet]
        public ActionResult LocationTemplate(int id)
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
                ViewBag.UID = id;
                ViewBag.Roles = db.Roles;
                ViewBag.Companies = db.Company.OrderBy(a => a.Name);
                ViewBag.Part = "Locations";
                if (id == 0)
                {

                    return View(new BusinessLocation { });
                }
                else
                {
                    BusinessLocation a = db.BusinessLocation.Find(id);
                    return View(a);
                }
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult LocationTemplate(int id, BusinessLocation u, HttpPostedFileBase ImageURL = null)
        {
            string fullPath = null;
            if (ImageURL != null)
            {


                var path = System.IO.Path.GetFileName(ImageURL.FileName);
                fullPath = "/Content/images/" + MD5Hasher.Hash(DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortDateString()) + path;
                fullPath = fullPath.Replace(" ", "_");
                ImageURL.SaveAs(Server.MapPath(fullPath));

            }
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
                if (id == 0)
                {
                    u.ImageURL = fullPath;
                    db.BusinessLocation.Add(u);
                }
                else
                {
                    db.BusinessLocation.Find(id).Name = u.Name;
                    db.BusinessLocation.Find(id).Email = u.Email;
                    db.BusinessLocation.Find(id).Description = u.Description;
                    db.BusinessLocation.Find(id).ImageURL = fullPath;
                    db.BusinessLocation.Find(id).IsPartner = u.IsPartner;
                    db.BusinessLocation.Find(id).Latitude = u.Latitude;
                    db.BusinessLocation.Find(id).Longtitude = u.Longtitude;
                    db.BusinessLocation.Find(id).PhoneNumber = u.PhoneNumber;
                }
                db.SaveChanges();
                return Redirect("~/Locations");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult LocationDelete(int id)
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
            ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                db.BusinessLocation.Remove(db.BusinessLocation.Find(id));
                db.SaveChanges();

                return Redirect("~/Locations");
            }
            else
                return Redirect("~/Login");
        }


        [HttpGet]
        public ActionResult PBFactTemplate(int id, int fact_id)
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
                ViewBag.PID = id;
                ViewBag.User = auth.GetCurrentUser(cookie)["User"];
                ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
                ViewBag.UID = id;
                ViewBag.Roles = db.Roles;
                ViewBag.Companies = db.Company.OrderBy(a => a.Name);
                ViewBag.Part = "Products";
                if (fact_id == 0)
                {

                    return View(new ProductBulletFact { });
                }
                else
                {
                    ProductBulletFact a = db.ProductBulletFact.Find(fact_id);
                    return View(a);
                }
            }
            else
                return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult PBFactTemplate(int id, int fact_id, ProductBulletFact u)
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
                if (fact_id == 0)
                {
                    u.ProductID = int.Parse(Request.Form["PID"]);
                    db.ProductBulletFact.Add(u);
                }
                else
                {
                    db.ProductBulletFact.Find(id).Fact = u.Fact;
                }
                db.SaveChanges();
                return Redirect("~/Product/" + int.Parse(Request.Form["PID"]) + "/Facts");
            }
            else
                return Redirect("~/Login");

        }
        public ActionResult PBFactDelete(int id, int fact_id)
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
            ViewBag.RoleNum = auth.GetCurrentUser(cookie)["Role"];
            ViewBag.User = auth.GetCurrentUser(cookie)["User"];
            ViewBag.Role = db.Roles.Find(int.Parse(auth.GetCurrentUser(cookie)["Role"])).Name;
            if (auth.CheckAuthStatus(uid, utoken) && auth.GetCurrentUser(cookie)["Role"] == "1")
            {
                db.ProductBulletFact.Remove(db.ProductBulletFact.Find(fact_id));
                db.SaveChanges();

                return Redirect("~/Product/" + id +"/Facts");
            }
            else
                return Redirect("~/Login");
        }

    }
}