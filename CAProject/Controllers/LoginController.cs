using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProject.Db;
using CAProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CAProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Auth(string email, string password, [FromServices] DbGallery db)
        {
            Debug.WriteLine($"email: {email} password: {password}");
            User user = db.Users.FirstOrDefault(x =>
                x.Email == email && x.Password == password);

            if (user == null)
            {
                ViewData["errMsg"] = "Email or incorrect password.";
                return View("Index");
            }
            else 
            {
                // Give User a new SessionId
                string guid = Guid.NewGuid().ToString();
                db.Sessions.Add(new Session()
                {
                    SessionId = guid,
                    UserId = user.Id
                });

                db.SaveChanges();

                HttpContext.Session.SetString("SessionId", guid);
                ViewData["SessionId"] = guid;

                return RedirectToAction("index", "home");
            }

        }

        public IActionResult Logout([FromServices] DbGallery db)
        {
            // Remove the SessionId
            string guid = HttpContext.Session.GetString("SessionId");
            Session session = db.Sessions.FirstOrDefault(x => x.SessionId == guid);
            db.Sessions.Remove(session);
            db.SaveChanges();
            HttpContext.Session.Clear();
            ViewData["SessionId"] = null;

            return RedirectToAction("Index", "Login");
        }
    }
}