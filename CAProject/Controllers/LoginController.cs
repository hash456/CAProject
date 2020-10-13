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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Localization;
using BC = BCrypt.Net.BCrypt;


namespace CAProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string FromCheckout)
        {
            
            ViewData["User"] = null;
            //Checking whether user accessed login from checkout button or not

            if (FromCheckout == "true")
            {
                HttpContext.Session.SetString("FromCheckout", "true");
            }
            else
            {
                HttpContext.Session.SetString("FromCheckout", "false");
            }
            Debug.WriteLine(HttpContext.Session.GetString("FromCheckout"));

            Debug.WriteLine(HttpContext.Session.GetString("RegisterSuccessful"));
            ViewData["newRegister"] = HttpContext.Session.GetString("RegisterSuccessful");
            HttpContext.Session.Remove("RegisterSuccessful");

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

            return View();
        }

        // Authorise User
        // Combine Temp Cart with User's Current Cart
        // Update user's current cart and remove items that is already sold out
        [HttpPost]
        public IActionResult Auth(string email, string password, [FromServices] DbGallery db)
        {
            Debug.WriteLine($"email: {email} password: {password}");
            User user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                ViewData["errMsg"] = "Email or incorrect password.";
                return View("Index");
            }
            string hashPassword = user.Password;
            bool verified = BC.Verify(password, hashPassword);

            if(verified == true) 
            {
                // Give User a new SessionId
                string guid = Guid.NewGuid().ToString();
                db.Sessions.Add(new Session()
                {
                    SessionId = guid,
                    UserId = user.Id
                });

                db.SaveChanges();

                // Get the Temp Cart
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

                // Get the unpaid order if it exist
                Order order = db.Orders.FirstOrDefault(x => x.UserId == user.Id && x.IsPaid == false);

                // Create new order is user don't have any order yet
                if (order == null && tempCart.Count > 0)
                {
                    db.Orders.Add(new Order
                    {
                        UserId = user.Id,
                        OrderDate = DateTime.Now.ToString()
                    });
                    db.SaveChanges();

                    Order newOrder = db.Orders.FirstOrDefault(x => x.UserId == user.Id && x.IsPaid == false);

                    foreach(Cart toAdd in tempCart)
                    {
                        db.Cart.Add(new Cart
                        {
                            OrderId = newOrder.Id,
                            ProductId = toAdd.ProductId,
                            Quantity = toAdd.Quantity
                        });
                        db.SaveChanges();
                    }
                }
                // Combine with unpaid order
                else if (tempCart.Count > 0)
                {
                    // Get the current unpaid order cart
                    List<Cart> cart = db.Cart.Where(x => x.OrderId == order.Id).ToList();

                    // Get a dictionary of unpaid order cart and the productID for easy lookup later
                    Dictionary<int, Cart> lookUpTable = new Dictionary<int, Cart>();
                    foreach (Cart cartItem in cart)
                    {
                        lookUpTable.Add(cartItem.ProductId, cartItem);
                    }

                    foreach (Cart toAdd in tempCart)
                    {
                        // See if the user already has the product in his cart
                        if (lookUpTable.ContainsKey(toAdd.ProductId))
                        {
                            Cart value = lookUpTable[toAdd.ProductId];
                            // Don't allow combination if the total count is more than our stock
                            int stockCount =
                                db.ActivationCode.Where(x => x.ProductId == value.ProductId && x.IsSold == false).Count();
                            if (value.Quantity + toAdd.Quantity <= stockCount)
                            {
                                value.Quantity += toAdd.Quantity;
                                db.SaveChanges();
                            } 
                            else if (value.Quantity + toAdd.Quantity > stockCount)
                            {
                                value.Quantity = stockCount;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            db.Cart.Add(new Cart
                            {
                                OrderId = order.Id,
                                ProductId = toAdd.ProductId,
                                Quantity = toAdd.Quantity
                            });
                            db.SaveChanges();
                        }
                    }
                }

                Debug.WriteLine(HttpContext.Session.GetString("FromCheckout"));
                string FromCheckout = HttpContext.Session.GetString("FromCheckout");

                // Clear the old session full of temp cart
                HttpContext.Session.Clear();
                // Add sessionId
                HttpContext.Session.SetString("SessionId", guid);
                ViewData["SessionId"] = guid;

                string updateCartMessage = "";

                // Check the user's current cart for sold out items
                Order prevOrder = db.Orders.FirstOrDefault(x => x.UserId == user.Id && x.IsPaid == false);
                if(prevOrder == null)
                {
                    updateCartMessage = "false";
                }
                else
                {
                    List<Cart> updateCart = db.Cart.Where(x => x.OrderId == prevOrder.Id).ToList();
                    // For each item in cart, check if we still have stock
                    foreach(Cart toUpdate in updateCart)
                    {
                        int currentStock = 
                            db.ActivationCode.Where(x => x.ProductId == toUpdate.ProductId && x.IsSold == false).Count();

                        // Remove/change the quantity based on how much stock we have left
                        if (currentStock == 0)
                        {
                            db.Cart.Remove(toUpdate);
                            updateCartMessage = "true";
                        }
                        else if (toUpdate.Quantity > currentStock)
                        {
                            toUpdate.Quantity = currentStock;
                            updateCartMessage = "true";
                        }
                        db.SaveChanges();

                        // Check if cart is empty after removing items, remove the orderid
                        if (db.Cart.FirstOrDefault(x => x.OrderId == prevOrder.Id) == null)
                        {
                            db.Orders.Remove(prevOrder);
                            db.SaveChanges();
                            updateCartMessage = "true";
                        }      
                    }
                }

                HttpContext.Session.SetString("updateCartMessage", updateCartMessage);

                //redirect to view cart page if checkout button was clicked in temp cart
                Debug.WriteLine(FromCheckout);


                if (FromCheckout == "true")
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    return RedirectToAction("index", "home");
                }
            }
            else
            {
                ViewData["errMsg"] = "Email or incorrect password.";
                return View("Index");
            }

        }

        public IActionResult Logout([FromServices] DbGallery db)
        {
            // Remove the SessionId from database
            string guid = HttpContext.Session.GetString("SessionId");
            Session session = db.Sessions.FirstOrDefault(x => x.SessionId == guid);
            db.Sessions.Remove(session);
            db.SaveChanges();

            // Remove the SessionId from Session Object
            HttpContext.Session.Clear();
            ViewData["SessionId"] = null;

            return RedirectToAction("Index", "Login");
        }
    }
}