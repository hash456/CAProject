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

namespace CAProj.Controllers
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

            return View();
        }
    }
}
