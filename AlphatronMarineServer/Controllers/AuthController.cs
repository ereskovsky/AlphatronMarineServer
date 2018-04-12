using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AlphatronMarineServer.Models;

namespace AlphatronMarineServer.Controllers
{
    public class AuthController : Controller
    {
        AlphatronMarineEntities db = new AlphatronMarineEntities();
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            int uid;
            string utoken;
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                uid = int.Parse(cookie["id"]);
                utoken = cookie["token"];
            }
            else
            {
                uid = 0;
                utoken = null;
            }
            if (CheckAuthStatus(uid, utoken))
            {

                return Redirect("~/Index");
            }
            else
                return View();
        }
        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("User");
            cookie.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(cookie);
            return Redirect("~/");

        }

        public ActionResult Authorize(string email, string password)
        {
            var pwd = MD5Hasher.Hash(password);
            var user = db.User.Where(a => a.Email == email && a.Password == pwd).FirstOrDefault();
            if (IfUserExists(email, password))
            {
                var token = CreateToken(email);
                var date = DateTime.Now;
                db.Auth.Add(new Auth { UserID = user.ID, Token = token, Date = date });
                db.SaveChanges();
                HttpCookie cookie = new HttpCookie("User");
                cookie["role"] = user.RoleID.ToString();
                cookie["token"] = token;
                cookie["user"] = user.Name;
                cookie["id"] = user.ID.ToString();
                cookie.Expires = DateTime.Now.AddHours(8);
                Response.Cookies.Add(cookie);
            }

            return Redirect("~/Index");
        }
        public bool IfUserExists(string email, string password)
        {
            var pwd = MD5Hasher.Hash(password);
            var user = db.User.Where(a => a.Email == email && a.Password == pwd).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool CheckAuthStatus(int id, string token)
        {
            int user_id;
            string token_;
            if (id != 0 && token != null)
            {
                user_id = id;
                token_ = token;

            }
            else
                return false;

            var query = db.Auth.Where(a => a.UserID == user_id && a.Token == token).ToList();
            if (query != null)
                return true;
            return true;
        }

        public static string CreateToken(string login)
        {
            var result = default(byte[]);

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write(DateTime.Now.Ticks);
                    writer.Write(login);
                }

                stream.Position = 0;

                using (var hash = SHA256.Create())
                {
                    result = hash.ComputeHash(stream);
                }
            }

            var text = new string[20];

            for (var i = 0; i < text.Length; i++)
            {
                text[i] = result[i].ToString("x2");
            }

            return string.Concat(text);
        }

        public Dictionary<string,string> GetCurrentUser(HttpCookie cookie)
        {

            Dictionary<string, string> cook = new Dictionary<string, string>();
            if (cookie != null)
            {
                cook.Add("User", cookie["user"]);
                cook.Add("Role", cookie["role"]);
                return cook;
            }
            else
                return null;

        }
    }
}