﻿using System;
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

        public IActionResult Index()
        {
            string sessionId = HttpContext.Session.GetString("SessionId");

            //int orderid = Convert.ToInt32(str_orderid);

            if (sessionId == null)
            {
                // Use session storage here if not logged in //
                return RedirectToAction("Index", "Login");
            }
            int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;

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
        //public IActionResult Index([FromBody]Order orders)
        //{
        //    int orderid = orders.Id;
        //    bool Paid = orders.IsPaid;

        //    return Json(new
        //    {
        //        status = "success"
        //    });
        //}

        //function Checkout()
        //{
        //    let xhr = new XMLHttpRequest();
        //    xhr.open("POST", "/Checkout/Index");
        //    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

        //    xhr.onreadystatechange = function() {
        //        if (this.status === 200 && data.status == "success")
        //        {
        //            {
        //                let data = JSON.parse(this.responseText);
        //                console.log = ("Operation: " + data.status);


        //            }
        //        }
        //        xhr.send(JSON.stringify({ Id: 3, IsPaid: true }));
        //    }
        //}

    }
}
