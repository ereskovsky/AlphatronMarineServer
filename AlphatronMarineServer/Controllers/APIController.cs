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
        //Read operations
        public string UsersVessels(int id)
        {
            return ApiModel.GetUsersVesselsList(id);
        }
        public string Equipments(int id)
        {
            return ApiModel.GetEquipmentByIMO(id);
        }
        public string CompaniesVessels(int id)
        {
            return ApiModel.GetVesselByCompanyID(id);
        }
        public string Products()
        {
            return ApiModel.GetProducts();
        }
        public string Locations()
        {
            return ApiModel.GetLocations();
        }
        public string GetUserInfo(int id)
        {
            return ApiModel.GetUser(id);
        }


    }
}