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
        AlphatronMarineEntities db = new AlphatronMarineEntities();
        public string UsersVessels(int user_id, string token, int id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return JsonConvert.SerializeObject(ApiModel.GetUsersVesselsList(id));
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
        public string ETFields(int user_id, string token, int id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.GetETFieldsBySerial(id);
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
        public string Logout(int user_id, string token)
        {
            Auth a = db.Auth.Where(x => x.UserID == user_id && x.Token == token).FirstOrDefault();
            if (a != null)
            {
                db.Auth.Remove(a);
                db.SaveChanges();
                return "Done";
            }
            else
            {
                return "No such user-token pair";
            }
            
        }

        public string StatusCheck(int user_id, string token)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.GetUserByID(user_id);
            }
            return "Not authorized";
        }
        public string EditVessel(int user_id, string token, string newobj)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.EditVessel(newobj);
            }
            return "Not authorized for this";
        }
        public string EditEquipment(int user_id, string token, string newobj)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.EditEquip(newobj);
            }
            return "Not authorized for this";
        }
        public string GetVesselByIMO(int user_id, string token, int imo)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.VesselByIMO(imo);
            }
            return "Not authorized for this";
        }
        public string GetTemp(int user_id, string token, int object_id)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.GetTemp(object_id);
            }
            return "Not authorized for this";
        }
        public string GetEquipmentBySerial(int user_id, string token, int serial)
        {
            if (auth.CheckAuthStatus(user_id, token))
            {
                return ApiModel.EquipmentBySerial(serial);
            }
            return "Not authorized for this";
        }

    }
}