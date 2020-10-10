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


            //Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.Id == orderid);
            int int_orderid = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false).Id;
            Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.Id == int_orderid && x.IsPaid == false);
            order.IsPaid = true;
            db.SaveChanges();

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
