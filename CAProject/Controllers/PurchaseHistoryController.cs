using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CAProject.Db;
using CAProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAProject.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        public IActionResult Index([FromServices] DbGallery db)
        {
            string sessionId = HttpContext.Session.GetString("SessionId");
            if (sessionId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Session session = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            User user = db.Users.FirstOrDefault(x => x.Id == session.UserId);
            ViewData["SessionId"] = sessionId;

            Session userr = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            ViewData["User"] = userr;

            List<Order> orders = db.Orders.Where(x => x.UserId == user.Id && x.IsPaid == true).ToList();
            if(orders.Count < 0)
            {
                ViewData["acLookup"] = new Dictionary<Product, List<ActivationCode>>();
                return View();
            }

            List<ActivationCode> acList = new List<ActivationCode>();
            foreach(Order order in orders)
            {
                List<ActivationCode> acs = db.ActivationCode.Where(x => x.OrderId == order.Id && x.IsSold == true).ToList();
                acList.AddRange(acs);
            }

            Dictionary<Order, List<Cart>> cartLookUp = new Dictionary<Order, List<Cart>>();
            foreach(Order order in orders)
            {
                List<Cart> cart = db.Cart.Where(x => x.OrderId == order.Id).ToList();
                cartLookUp.Add(order, cart);
            }

            Dictionary<Product, List<ActivationCode>> acLookUp = new Dictionary<Product, List<ActivationCode>>();
            foreach(ActivationCode ac in acList)
            {
                if (acLookUp.ContainsKey(ac.Product))
                {
                    acLookUp[ac.Product].Add(ac);
                }
                else
                {
                    acLookUp.Add(ac.Product, new List<ActivationCode> { ac });
                }
            }

            ViewData["Order"] = orders;
            ViewData["acLookup"] = acLookUp;
            ViewData["cartLookup"] = cartLookUp;

            // Display bubble using user's cart
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;
            Order orderBubble = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);
            if (orderBubble != null)
            {
                List<Cart> cart = db.Cart.Where(x => x.OrderId == orderBubble.Id).ToList();
                ViewData["Cart"] = cart;
            }
            else
            {
                ViewData["Cart"] = null;
            }

            return View();
        }
    }
}
