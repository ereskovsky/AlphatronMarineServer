using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AlphatronMarineServer.Models;

namespace AlphatronMarineServer.Controllers
{
    public class APIController : Controller
    {
        public string Vessels(int id)
        {
            return ApiModel.GetVesselsList;
        }
        public string Equipments(int id)
        {
            return ApiModel.GetEquipmentByIMO(id);
        }
    }
}