﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphatronMarineServer.Models;

namespace AlphatronMarineServer.Controllers
{
    public class HomeController : Controller
    {
        AlphatronMarineEntities db = new AlphatronMarineEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fleet()
        {
            ViewBag.Vessels = db.Vessel;
            return View();
        }
        
    }
}