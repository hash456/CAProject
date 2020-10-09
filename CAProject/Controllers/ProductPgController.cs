using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAProject.Models;
using CAProject.Db;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace CAProject.Controllers
{
    public class ProductPgController : Controller
    {
        private readonly DbGallery db;
        public ProductPgController(DbGallery db)
        {
            this.db = db;
        }

        public IActionResult Index(int productid)
        {
            var product = db.Product.Where(
                x => x.Id == productid).SingleOrDefault();
            ViewData["product"] = product;

            // Get reviews related to the product from Review Table
            var reviews = db.Review.Where(
                x => x.ProductId == product.Id).ToList();
            ViewData["reviews"] = reviews;

            int numReviews = Convert.ToInt32(reviews.Count());
            ViewData["numReviews"] = numReviews;

            if (numReviews != 0)
            {
                double avgScore = Convert.ToDouble(reviews.Average(x => x.Rating));
                ViewData["avgScore"] = avgScore;
            } else
            {
                ViewData["avgScore"] = (double)0;
            }

            // Get stock count and number sold from ActivationCode Table
            int stockCount = Convert.ToInt32(db.ActivationCode.Where(
                x => x.ProductId == productid && x.IsSold == false)
                .Count());
            ViewData["stockCount"] = stockCount;

            int numSold = Convert.ToInt32(db.ActivationCode.Where(
                x => x.ProductId == productid && x.IsSold == true)
                .Count());
            ViewData["numSold"] = numSold;

            ViewData["SessionId"] = HttpContext.Session.GetString("SessionId");

            return View();
        }
    }
}
