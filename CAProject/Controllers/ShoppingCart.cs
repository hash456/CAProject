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
using System.ComponentModel.Design;
using System.Diagnostics;

namespace CAProject.Controllers
{
    public class ShoppingCart : Controller
    {
        private readonly DbGallery db;

        public ShoppingCart(DbGallery db)
        {
            this.db = db;
        }

        // Display Shopping Cart
        public IActionResult Index()
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");
            List<Cart> cart = new List<Cart>();
            // No sessionId = user not logged in = don't allow them to add to cart for now
            if (sessionId == null)
            {
                
                ViewData["User"] = null;
                // Use session storage here if not logged in -- this is the view//

                //storage now is "productid, quantity"

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Product0")))
                {
                    ViewData["Cart"] = new List<Cart>();
                    ViewData["Product"] = new List<Product>();
                    ViewData["StockCount"] = new Dictionary<int, int>();
                    ViewData["TotalCost"] = (double)0;
                    ViewData["OrderId"] = (int)-1;
                    return View();
                }

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

                //CREATE A LIST OF CART ITEMS CALLED CART
                

                foreach (string x in items)
                {
                    if(x != "removed product")
                    {
                        string[] xx = x.Split(',');
                        Cart y = new Cart();
                        y.ProductId = Convert.ToInt32(xx[0]);
                        y.Quantity = Convert.ToInt32(xx[1]);
                        cart.Add(y);
                    }
                }

                ViewData["OrderId"] = (int)-1;

            }
            else
            {
                int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;
                ViewData["SessionId"] = sessionId;

                Session user = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
                ViewData["User"] = user;

                // Combine the session and db cart //

                // Get the Order ID to access the cart
                Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);

                // Return empty cart if no order id is found
                if (order == null)
                {
                    ViewData["Cart"] = new List<Cart>();
                    ViewData["Product"] = new List<Product>();
                    ViewData["StockCount"] = new Dictionary<int, int>();
                    ViewData["TotalCost"] = (double)0;
                    ViewData["OrderId"] = (int)-1;
                    return View();
                }

                // Get all the items in the cart belonging to that orderId
                cart = db.Cart.Where(x => x.OrderId == order.Id).ToList();
                ViewData["OrderId"] = order.Id;
            }

            //SYNC BACK HERE
            // Get the stock count for each items in the cart
            Dictionary<int, int> stockCount = new Dictionary<int, int>();
            foreach(Cart item in cart)
            {
                int count = db.ActivationCode.Where(x => x.ProductId == item.ProductId && x.IsSold == false).Count();
                stockCount.Add(item.ProductId, count - item.Quantity);
            }

            double totalCost = 0;
            foreach (Cart item in cart)
            {
                if (item.Product != null)
                {
                    totalCost += item.Product.Price * item.Quantity;
                }
                else
                {
                    Product itemprice = db.Product.Where(x => x.Id == item.ProductId).SingleOrDefault();
                    totalCost += itemprice.Price * item.Quantity;
                }                 
            }
            List<Product> products = new List<Product>();
            for (int i = 0; i < cart.Count; i++)
            {
                Product product = db.Product.FirstOrDefault(x => x.Id == cart[i].ProductId);
                products.Add(product);
            }
            ViewData["Product"] = products;
            ViewData["TotalCost"] = totalCost;
            ViewData["Cart"] = cart;
            
            ViewData["StockCount"] = stockCount;

            int totalCostmulti100 = Convert.ToInt32(totalCost * 100);
            HttpContext.Session.SetInt32("TotalCost", totalCostmulti100);

            return View();
        }

        // Add items to shopping cart from Home Page and Product Details Page
        public IActionResult UpdateCart([FromBody] CartInput cartInput)
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");
            string message = "Items have been added to cart";

            // Get the product Id from cartInput
            int productId = Convert.ToInt32(cartInput.ProductId);
            int quantity = Convert.ToInt32(cartInput.Quantity);

            if (sessionId == null)
            {

                int i = 0;
                string newcartitem = Convert.ToString(productId) + "," + Convert.ToString(quantity);
                
                // Get all the temp cart items
                // items is a dictionary of key = "productId,quanty", value = "Product{j}"
                Dictionary<string, string> items = new Dictionary<string, string>();
                int j = 0;
                string item = "initiate";
                do
                {
                    item = HttpContext.Session.GetString("Product" + Convert.ToString(j));
                    if (item == null)
                        break;
                    if(item != "removed product")
                        items.Add(item, "Product" + Convert.ToString(j));
                    j++;
                } while (item != null);


                // Find if the productId is in the session already
                bool exist = false;
                string[] tempProductId = new string[2]; 
                string tempCartIndex = ""; 
                foreach (KeyValuePair<string,string> entry in items)
                {
                    tempProductId = entry.Key.Split(",");
                    tempCartIndex = entry.Value;
                    if(Convert.ToInt32(tempProductId[0]) == productId)
                    {
                        exist = true;
                        break;
                    }
                }

                // If productId is found in the session
                if (exist)
                {
                    string toUpdate = HttpContext.Session.GetString(tempCartIndex);
                    string[] tempproductIdQty = toUpdate.Split(",");
                    int tempproductId = Convert.ToInt32(tempproductIdQty[0]);
                    int tempQty = Convert.ToInt32(tempproductIdQty[1]);
                    // Check is quantity is more that what we have in stock
                    if (tempQty + quantity <= db.ActivationCode.Where(x => x.ProductId == tempproductId && x.IsSold == false).Count())
                    {
                        tempQty += quantity;
                    }
                    else
                    {
                        tempQty = db.ActivationCode.Where(x => x.ProductId == tempproductId && x.IsSold == false).Count();
                        message = "Reached maximum stock";
                    }
                    // Remove the old session storage and add the new one
                    HttpContext.Session.Remove(tempCartIndex);
                    string toAdd = Convert.ToString(tempproductId) + "," + Convert.ToString(tempQty);
                    HttpContext.Session.SetString(tempCartIndex, toAdd);
                }
                // If productId is not found in the session
                else
                {
                    while (string.IsNullOrEmpty(HttpContext.Session.GetString("Product" + Convert.ToString(i))) == false)
                    {
                        i++;
                    }

                    HttpContext.Session.SetString("Product" + Convert.ToString(i), newcartitem);
                }
            }
            

            else
            {
                int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;



                // Check if the user currently as an order
                // Create a new order if user currently don't have any order yet
                if (db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false) == null)
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
                        Quantity = quantity
                    };
                    db.Cart.Add(cart);
                }
                // Don't allow user to add more that what we have in stock
                else if (cart.Quantity + quantity <= db.ActivationCode.Where(x => x.ProductId == cart.ProductId && x.IsSold == false).Count())
                {
                    cart.Quantity += quantity;
                }
                else
                {
                    cart.Quantity = db.ActivationCode.Where(x => x.ProductId == cart.ProductId && x.IsSold == false).Count();
                    message = "Reached maximum stock";
                }

                db.SaveChanges(); 
            }
            
            return Json(new
            {
                status = "success",
                message
            });
        }

        // Edit items
        public IActionResult ChangeCartQty([FromBody] ChangeQtyInput changeQtyInput)
        {
            // Get the User ID for the current session
            string sessionId = HttpContext.Session.GetString("SessionId");

            // Get the productId from string to int
            int productIdNum = Convert.ToInt32(changeQtyInput.ProductId);

            // No sessionId = user not logged in = don't allow them to add to cart for now
            if (sessionId == null)
            {
                // Get all the temp cart items
                // items is a dictionary of key = "productId,quanty", value = "Product{j}"
                Dictionary<string, string> items = new Dictionary<string, string>();
                int j = 0;
                string item = "initiate";
                do
                {
                    item = HttpContext.Session.GetString("Product" + Convert.ToString(j));
                    if (item == null)
                        break;
                    if (item != "removed product")
                        items.Add(item, "Product" + Convert.ToString(j));
                    j++;
                } while (item != null);

                // Find the item the user is trying to update
                bool found = false;
                string[] tempProductId = new string[2];
                string tempCartIndex = "";
                foreach (KeyValuePair<string, string> entry in items)
                {
                    tempProductId = entry.Key.Split(",");
                    tempCartIndex = entry.Value;
                    if (Convert.ToInt32(tempProductId[0]) == productIdNum)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    string toUpdate = HttpContext.Session.GetString(tempCartIndex);
                    string[] tempproductIdQty = toUpdate.Split(",");
                    int tempproductId = Convert.ToInt32(tempproductIdQty[0]);
                    int tempQty = Convert.ToInt32(tempproductIdQty[1]);
                    if (changeQtyInput.Action == "minus" && tempQty > 1)
                    {
                        tempQty--;
                        HttpContext.Session.Remove(tempCartIndex);
                        string toAdd = Convert.ToString(tempproductId) + "," + Convert.ToString(tempQty);
                        HttpContext.Session.SetString(tempCartIndex, toAdd);
                    }
                    else if (changeQtyInput.Action == "plus" &&
                        tempQty < db.ActivationCode.Where(x => x.ProductId == tempproductId && x.IsSold == false).Count())
                    {
                        tempQty++;
                        HttpContext.Session.Remove(tempCartIndex);
                        string toAdd = Convert.ToString(tempproductId) + "," + Convert.ToString(tempQty);
                        HttpContext.Session.SetString(tempCartIndex, toAdd);
                    }
                    else if (changeQtyInput.Action == "remove")
                    {
                        HttpContext.Session.Remove(tempCartIndex);
                        // Even if we remove the product, still need a session of "Product{j}" or else will have errors
                        HttpContext.Session.SetString(tempCartIndex, "removed product");
                    }

                    return Json(new { status = "success" });
                }
                else
                {
                    return Json(new { status = "error" });
                }
            }
            else
            {
                int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;

                // Get the order id
                Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);

                // Find the product in the user cart
                Cart cart = db.Cart.FirstOrDefault(x => x.ProductId == productIdNum && x.OrderId == order.Id);

                if (changeQtyInput.Action == "minus" && cart.Quantity > 1)
                {
                    cart.Quantity--;
                }
                else if (changeQtyInput.Action == "plus" &&
                    cart.Quantity < db.ActivationCode.Where(x => x.ProductId == cart.ProductId && x.IsSold == false).Count())
                {
                    cart.Quantity++;
                }
                else if (changeQtyInput.Action == "remove")
                {
                    db.Cart.Remove(cart);
                    db.SaveChanges();

                    // Check if cart is empty after removing
                    if (db.Cart.FirstOrDefault(x => x.OrderId == order.Id) == null)
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

        //Paypal Payment
        public IActionResult Paypal()
        {
            string sessionId = HttpContext.Session.GetString("SessionId");

            if (sessionId == null)
            {
                return RedirectToAction("Index", "Login", new { FromCheckout = "true" });
            }

            int totalcostmulti = (int)HttpContext.Session.GetInt32("TotalCost");
            double totalcost = totalcostmulti / 100.0;

            ViewData["TotalCost"] = totalcost;
            Session user = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            ViewData["SessionId"] = sessionId;
            ViewData["User"] = user;
            return View();
        }

    }
}
