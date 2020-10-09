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

            // Get all the items in the cart belonging to that user
            List<Cart> cart = db.Cart.Where(x => x.UserId == userId).ToList();

            // Get the stock count for each items in the cart
            Dictionary<int, int> stockCount = new Dictionary<int, int>();
            foreach(Cart item in cart)
            {
                int count = db.ActivationCode.Where(x => x.ProductId == item.ProductId && x.IsSold == false).Count();
                stockCount.Add(item.ProductId, count);
            }

            /*
            List<Product> products = new List<Product>();

            for (int i = 0; i < Cart.Count; i++)
            {
                Product product = db.Product.FirstOrDefault(x => x.Id == int.Parse(Cart[i].ProductId));
                products.Add(product);
            }
            ViewData["Cart"] = products;
            */

            ViewData["Cart"] = cart;
            ViewData["StockCount"] = stockCount;
            ViewData["SessionId"] = sessionId;

            return View();
        }

        public IActionResult UpdateCart([FromBody] CartInput cartInput)
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");

            // No sessionId = user not logged in = don't allow them to add to cart for now
            if (sessionId == null)
            {
                return Json(new { status = "error" });
            }
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;

            // Get the product Id from cartInput
            int productId = Convert.ToInt32(cartInput.ProductId);

            // Check if the current item is already in the cart
            Cart cart = db.Cart.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
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
            
            db.SaveChanges();
            
            return Json(new
            {
                status = "success"
            });
        }
    }
}
