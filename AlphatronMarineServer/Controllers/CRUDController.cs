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
        [HttpGet]
        public ActionResult VesselTemplate(int id)
        {
            ViewBag.Countries = db.Country;
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

        [HttpPost]
        public ActionResult VesselTemplate(int id, Vessel v)
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
            }
            db.SaveChanges();
            return Redirect("~/Fleet");
            

        }
    }
}