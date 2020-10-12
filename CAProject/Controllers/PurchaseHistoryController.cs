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

            ViewData["acLookup"] = acLookUp;

            return View();
        }
    }
}
