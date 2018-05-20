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
        static AuthController auth = new AuthController();
        static AlphatronMarineEntities db = new AlphatronMarineEntities();
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
        public static string GetETFieldsBySerial(int id)
        {
            var c = db.Equipment.Find(id);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c.FieldsValues);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";


        }
        public static string GetUserByID(int user_id)
        {
            var c = db.User.Find(user_id);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
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
            if (auth.IfUserExists(email, password))
            {
                var pwd = MD5Hasher.Hash(password);
                var c = db.User.Where(a => a.Email == email && a.Password == password).FirstOrDefault();
                if (c != null)
                {
                    var token = auth.API(email, password);
                    AuthApiResponse resp = new AuthApiResponse { User = c, Token = token };
                    var encoded = JsonConvert.SerializeObject(resp);
                    return encoded;
                }
                return "Something went wrong ¯\\_(ツ)_/¯ ";
            }
            else
                return "User does not exist";
        }

        public static string EditVessel(string newobj)
        {
            Vessel v = JsonConvert.DeserializeObject<Vessel>(newobj);
            if (db.Vessel.Find(v.IMO) != null)
            {
                db.Temp.Add(new Temp { ObjectID = v.IMO, Type = "Vessel", SerializedObject = newobj});
                db.SaveChanges();
                return "Changes added to queue";
            }
            else
            return "There is no vessel with such IMO";
        }

        public static string EditEquip(string newobj)
        {
            Equipment v = JsonConvert.DeserializeObject<Equipment>(newobj);
            if (db.Vessel.Find(v.SerialNumber) != null)
            {
                
                db.Temp.Add(new Temp { ObjectID = v.SerialNumber, Type = "Equipment", SerializedObject = newobj });
                db.SaveChanges();
                return "Changes added to queue";
            }
            else
                return "There is no equipment with such serial";
        }


        public static string VesselByIMO(int imo)
        {
            var c = db.Vessel.Find(imo);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
        }
        public static string EquipmentBySerial(int serial)
        {
            var c = db.Equipment.Find(serial);
            if (c != null)
            {
                var encoded = JsonConvert.SerializeObject(c);
                return encoded;
            }
            return "Something went wrong ¯\\_(ツ)_/¯ ";
        }



    }
    public class AuthApiResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Date { get { return DateTime.Now; }}
    }
}