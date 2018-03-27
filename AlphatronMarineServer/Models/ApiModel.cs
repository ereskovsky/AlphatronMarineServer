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
        public static string GetVesselsList()
        {
            var list = db.Vessel.ToList();
            var encoded = JsonConvert.SerializeObject(list);
            return encoded;

        }
        public static string GetVesselByIMO(int id)
        {
            var v = db.Vessel.Find(id);
            var encoded = JsonConvert.SerializeObject(v);
            return encoded;

        }
       
        public static string GetCompanyByID(int id)
        {
            var c = db.Company.Find(id);
            var encoded = JsonConvert.SerializeObject(c);
            return encoded;

        }

    }
}