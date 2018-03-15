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
        // GET: CRUD
        public ActionResult FleetTemplate()
        {
            ViewBag.Countries = db.Country;

            return View();
        }
    }
}