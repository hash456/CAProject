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

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 9;

            if (HttpContext.Session.GetString("LoggedIn") != null)
            {
                ViewData["DisplayLogout"] = "YES";
            }

            PaginatedList<Product> paginatedList = await PaginatedList<Product>.CreateAsync(db.Product, pageNumber ?? 1, pageSize);
            ViewData["paginatedList"] = paginatedList;

            Dictionary<int, int> paginatedStockCount = new Dictionary<int, int>();
            foreach(Product p in paginatedList)
            {
                paginatedStockCount.Add(p.Id, Convert.ToInt32(db.ActivationCode.Where(
                                                    x => x.ProductId == p.Id && x.IsSold == true).Count()));
            }

            ViewData["paginatedStockCount"] = paginatedStockCount;

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
