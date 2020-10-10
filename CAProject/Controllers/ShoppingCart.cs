using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProject.Db;
using Microsoft.AspNetCore.Mvc;
using CAProject.Models;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace CAProject.Controllers
{
    public class ShoppingCart : Controller
    {
        private readonly DbGallery db;

        public ShoppingCart(DbGallery db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");

            // No sessionId = user not logged in = don't allow them to add to cart for now
            if(sessionId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;
            ViewData["SessionId"] = sessionId;

            // Get the Order ID to access the cart
            Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);

            // Return empty cart if no order id is found
            if(order == null)
            {
                ViewData["Cart"] = new List<Cart>();
                ViewData["StockCount"] = new Dictionary<int, int>();
                return View();
            }

            // Get all the items in the cart belonging to that orderId
            List<Cart> cart = db.Cart.Where(x => x.OrderId == order.Id).ToList();

            // Get the stock count for each items in the cart
            Dictionary<int, int> stockCount = new Dictionary<int, int>();
            foreach(Cart item in cart)
            {
                int count = db.ActivationCode.Where(x => x.ProductId == item.ProductId && x.IsSold == false).Count();
                stockCount.Add(item.ProductId, count);
            }

            ViewData["Cart"] = cart;
            ViewData["StockCount"] = stockCount;

            return View();
        }

        public IActionResult UpdateCart([FromBody] CartInput cartInput)
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");
            string message = "Items have been added to cart";

            // No sessionId = user not logged in = don't allow them to add to cart for now
            if (sessionId == null)
            {
                return Json(new { status = "error" });
            }
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;

            // Get the product Id from cartInput
            int productId = Convert.ToInt32(cartInput.ProductId);

            // Check if the user currently as an order
            // Create a new order if user currently don't have any order yet
            if(db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false) == null)
            {
                db.Orders.Add(new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now.ToString()
                });

                db.SaveChanges();
            }

            // Get the order id
            Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);

            // Check if the current item is already in the cart
            Cart cart = db.Cart.FirstOrDefault(x => x.ProductId == productId && x.OrderId == order.Id);

            if (cart == null)
            {
                cart = new Cart()
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                db.Cart.Add(cart);
            }
            // Don't allow user to add more that what we have in stock
            else if (cart.Quantity < db.ActivationCode.Where(x => x.ProductId == cart.ProductId && x.IsSold == false).Count())
            {
                cart.Quantity++;
            } 
            else
            {
                message = "Reached maximum stock";
            }
            
            db.SaveChanges();
            
            return Json(new
            {
                status = "success",
                message
            });
        }

        public IActionResult ChangeCartQty([FromBody] ChangeQtyInput changeQtyInput)
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");

            // No sessionId = user not logged in = don't allow them to add to cart for now
            if (sessionId == null)
            {
                return Json(new { status = "error" });
            }
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;

            // Get the order id
            Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);

            // Get the productId from string to int
            int productIdNum = Convert.ToInt32(changeQtyInput.ProductId);

            // Find the product in the user cart
            Cart cart = db.Cart.FirstOrDefault(x => x.ProductId == productIdNum && x.OrderId == order.Id);

            if(changeQtyInput.Action == "minus" && cart.Quantity > 1)
            {
                cart.Quantity--;
            }
            else if(changeQtyInput.Action == "plus" && 
                cart.Quantity < db.ActivationCode.Where(x => x.ProductId == cart.ProductId && x.IsSold == false).Count())
            { 
                cart.Quantity++;
            } 
            else if(changeQtyInput.Action == "remove")
            {
                db.Cart.Remove(cart);
                db.SaveChanges();

                // Check if cart is empty after removing
                if(db.Cart.FirstOrDefault(x => x.OrderId == order.Id) == null)
                {
                    // If empty, just remove the order id alltogether
                    db.Orders.Remove(order);
                }
            }

            db.SaveChanges();

            return Json(new
            {
                status = "success"
            });
        }
    }
}
