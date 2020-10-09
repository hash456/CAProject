using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProject.Db;
using Microsoft.AspNetCore.Mvc;
using CAProject.Models;
using System.Runtime.InteropServices.ComTypes;

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
            List<Cart> Cart = db.Cart.ToList();
            List<Product> products = new List<Product>();
            for (int i = 0; i < Cart.Count; i++)
            {
                Product product = db.Product.FirstOrDefault(x => x.Id == int.Parse(Cart[i].ProductId));
                products.Add(product);
            }
            ViewData["Cart"] = products;
            return View();
        }

        public IActionResult UpdateCart([FromBody] CartInput cartInput)
        {
            Cart cart = db.Cart.FirstOrDefault(x => x.ProductId == cartInput.ProductId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    ProductId = cartInput.ProductId,
                    Quantity = 1
                };
                db.Cart.Add(cart);
            }
            else
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
