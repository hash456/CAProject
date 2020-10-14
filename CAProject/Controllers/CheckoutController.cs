using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CAProject.Models;
using CAProject.Db;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace CAProject.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DbGallery db;
        public CheckoutController(DbGallery db)
        {
            this.db = db;
        }

        // Update the database on item sold on checkout
        public IActionResult Index()
        {
            string sessionId = HttpContext.Session.GetString("SessionId");

            //int orderid = Convert.ToInt32(str_orderid);

            if (sessionId == null)
            {
                // Use session storage here if not logged in //
                return RedirectToAction("Index", "Login", new { FromCheckout = "true" });
            }
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;

            Session user = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            ViewData["User"] = user;

            // Stop the user from submitting an empty shopping cart
            Order exist = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);
            if(exist == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            int int_orderid = exist.Id;

            Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.Id == int_orderid && x.IsPaid == false);
            order.IsPaid = true;
            order.CheckOutDate = DateTime.Now.ToString();
            db.SaveChanges();

            // Get the List Cart items of that orderId
            List<Cart> paidItems = db.Cart.Where(x => x.Order.Id == int_orderid).ToList();
            // Mark the activation code as sold and record it as sold to which orderId to display it for the customer later
            foreach(Cart item in paidItems)
            {
                for(int i = 0; i < item.Quantity; i++)
                {
                    ActivationCode activationCode = 
                        db.ActivationCode.FirstOrDefault(x => x.ProductId == item.ProductId && x.IsSold == false);

                    // error is product is already sold

                    activationCode.IsSold = true;
                    activationCode.OrderId = int_orderid;
                    db.SaveChanges();
                }
            }     

            order = db.Orders.FirstOrDefault(x => x.UserId == userId);
            if (order.IsPaid)
            {
                Debug.WriteLine("paid");
            }
            ViewData["SessionId"] = sessionId;

            return View();
        }
    }
}
