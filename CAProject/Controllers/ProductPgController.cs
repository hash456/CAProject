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

            return View();
        }
    }
}
