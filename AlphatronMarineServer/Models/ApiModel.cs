using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlphatronMarineServer.Controllers;

namespace AlphatronMarineServer.Models
{
    public class ApiModel
    {
        static AlphatronMarineEntities db = new AlphatronMarineEntities();
        AuthController auth = new AuthController();
        public static string GetUsersVesselsList(int id)
        {
            List<Vessel> vessels = new List<Vessel>();
            var q = db.VesselAccess.Where(x => x.SuperIntendantID == id);
            if (q != null) {
                foreach (var ves in q)
                {
                    vessels.Add(ves.Vessel);
                }
            }
            if (vessels != null)
            {
                var encoded = JsonConvert.SerializeObject(vessels);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";

        }
        public static string GetVesselByIMO(int id)
        {
            var v = db.Vessel.Find(id);
            if (v != null)
            {
                var encoded = JsonConvert.SerializeObject(v);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";

        }
        public static string GetVesselByCompanyID(int id)
        {
            var v = db.Vessel.Where(x=>x.CompanyID == id).ToList();
            if (v != null)
            {
                var encoded = JsonConvert.SerializeObject(v);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";

        }

        public static string GetCompanyByID(int id)
        {
            var c = db.Company.Find(id);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯";

        }
        public static string GetEquipmentByIMO(int imo)
        {
            var c = db.Vessel.Find(imo);
            if (c!= null) {
                var encoded = JsonConvert.SerializeObject(c.Equipment);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
            

        }
        public static string GetProducts()
        {
            var c = db.Product.ToList();
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
        }
        public static string GetLocations()
        {
            var c = db.BusinessLocation.ToList();
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
        }
        public static string GetUserInfo(int id)
        {
            var c = db.User.Find(id);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
        }
        public static string AuthUser(string email, string password)
        {
            var c = db.User.Find(id);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
        }

    }
}