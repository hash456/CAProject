using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProject.Db;
using CAProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;


namespace CAProject.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            // Get the Temp Cart to display bubble
            List<string> items = new List<string>();
            int j = 0;
            string item = "initiate";
            do
            {
                item = HttpContext.Session.GetString("Product" + Convert.ToString(j));
                if (item == null)
                    break;
                items.Add(item);
                j++;
            } while (item != null);

            List<Cart> tempCart = new List<Cart>();
            foreach (string x in items)
            {
                if (x != "removed product")
                {
                    string[] xx = x.Split(',');
                    Cart y = new Cart();
                    y.ProductId = Convert.ToInt32(xx[0]);
                    y.Quantity = Convert.ToInt32(xx[1]);
                    tempCart.Add(y);
                }
            }

            if (tempCart.Count > 0)
            {
                ViewData["Cart"] = tempCart;
            }
            else
            {
                ViewData["Cart"] = null;
            }
            
            ViewData["User"] = null;

            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(string name, string email, string password, string confirmPassword, [FromServices] DbGallery db)
        {
            if(password == confirmPassword)
            {
                db.Users.Add(new User { 
                    Name = name,
                    Email = email,
                    Password = BC.HashPassword(password)
                });

                db.SaveChanges();

                HttpContext.Session.SetString("RegisterSuccessful", "true");

                return RedirectToAction("Index", "Login", new { FromCheckout = "false" });
            }
            else
            {
                ViewData["RegErrMsg"] = "Password does not match";
                return View("Index");
            }
        }
    }
}
