using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphatronMarineServer.Models
{
    public class ApiModel
    {
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
            var encoded = JsonConvert.SerializeObject(vessels);
            return encoded;

        }
        public static string GetVesselByIMO(int id)
        {
            var v = db.Vessel.Find(id);
            var encoded = JsonConvert.SerializeObject(v);
            return encoded;

        }
        public static string GetVesselByCompanyID(int id)
        {
            var v = db.Vessel.Where(x=>x.CompanyID == id).ToList();
            var encoded = JsonConvert.SerializeObject(v);
            return encoded; 

        }

        public static string GetCompanyByID(int id)
        {
            var c = db.Company.Find(id);
            var encoded = JsonConvert.SerializeObject(c);
            return encoded;

        }
        public static string GetEquipmentByIMO(int imo)
        {
            var c = db.Vessel.Find(imo);
            var encoded = JsonConvert.SerializeObject(c.Equipment);
            return encoded;

        }
        public static string GetProducts()
        {
            var c = db.Product.ToList();
            var encoded = JsonConvert.SerializeObject(c);
            return encoded;
        }
        public static string GetLocations()
        {
            var c = db.BusinessLocation.ToList();
            var encoded = JsonConvert.SerializeObject(c);
            return encoded;
        }

    }
}