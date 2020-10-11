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
using Microsoft.AspNetCore.Http;

namespace CAProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbGallery db;

        public HomeController(ILogger<HomeController> logger, DbGallery db)
        {
            _logger = logger;
            this.db = db;
        }

        // Display Home Page
        public async Task<IActionResult> Index(int? pageNumber, string search)
        {
            var products = from p in db.Product
                           select p;

            // Search functionality
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(x => x.Name.Contains(search) || x.Description.Contains(search));
            }

            int pageSize = 9;

            // Get Paginated Result
            PaginatedList<Product> paginatedList = await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, pageSize);
            ViewData["paginatedList"] = paginatedList;

            // Get stock count for each product
            Dictionary<int, int> paginatedStockCount = new Dictionary<int, int>();
            foreach(Product p in paginatedList)
            {
                paginatedStockCount.Add(p.Id, Convert.ToInt32(db.ActivationCode.Where(
                                                    x => x.ProductId == p.Id && x.IsSold == false).Count()));
            }

            ViewData["paginatedStockCount"] = paginatedStockCount;
            ViewData["SessionId"] = HttpContext.Session.GetString("SessionId");
            ViewData["changedCart"] = HttpContext.Session.GetString("updateCartMessage");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
