using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlphatronMarineServer.Models;
using Newtonsoft.Json;

namespace AlphatronMarineServer.Controllers
{
    public class APIController : Controller
    {
        AuthController auth = new AuthController();
        //Read operations
        
        public string UsersVessels(int user_id, string token, int id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.GetUsersVesselsList(id);
            }
            return "Not authorized for this";
        }
        public string Equipments(int user_id, string token, int id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.GetEquipmentByIMO(id);
            }
            return "Not authorized for this";
        }
        public string CompaniesVessels(int user_id, string token, int id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
               return ApiModel.GetVesselByCompanyID(id);
            }
            return "Not authorized for this";
        }
        public string Products()
        {
           return ApiModel.GetProducts();           
        }
        public string Locations()
        {
           return ApiModel.GetLocations();
        }
        public string GetUserInfo(int user_id, string token, int id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.GetUserInfo(id);
            }
            return "Not authorized for this";
        }
        public string APIAuth(string email, string password)
        {
            var z = ApiModel.AuthUser(email, password);
            return ApiModel.AuthUser(email, password);
        }
        public string test()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("1 Field", "lol");
            dic.Add("2 Field", "kek");
            dic.Add("3 Field", "cheburek");

            return JsonConvert.SerializeObject(dic);
        }


    }
}