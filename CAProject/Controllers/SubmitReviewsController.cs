using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProject.Db;
using CAProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAProject.Controllers
{
    public class SubmitReviewsController : Controller
    {
        [HttpPost]
        public IActionResult Index(int productid, int rating, string review, [FromServices] DbGallery db)
        {
            string sessionId = HttpContext.Session.GetString("SessionId");
            if (sessionId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Session session = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            User user = db.Users.FirstOrDefault(x => x.Id == session.UserId);
            ViewData["SessionId"] = sessionId;

            db.Review.Add(new Review
            {
                UserName = user.Name,
                MainReview = review,
                Rating = rating,
                DateReviewed = DateTime.Now.ToString(),
                ProductId = productid
            });

            db.SaveChanges();

            return RedirectToAction("Index", "ProductPg", new { productid = productid });
        }
    }
}
