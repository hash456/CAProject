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

        // Display Product Details Page
        public IActionResult Index(int productid)
        {
            var product = db.Product.Where(
                x => x.Id == productid).SingleOrDefault();
            ViewData["product"] = product;

            // Get reviews related to the product from Review Table
            var reviews = db.Review.Where(
                x => x.ProductId == product.Id).ToList();
            ViewData["reviews"] = reviews;

            // Get all reviews
            List<Review> allReviews = db.Review.ToList();

            // Get a list of product with reviews
            List<int> hasReview = new List<int>();
            hasReview = db.Review.Select(x => x.ProductId).Distinct().ToList();

            // Get avg ratings for each product
            List<double> avgRatings = new List<double>();
            for (int i = 0; i < hasReview.Count; i++)
            {
                var review_each = db.Review.Where(x => x.ProductId == hasReview[i]);
                double avgnum = Convert.ToDouble(review_each.Average(x => x.Rating));
                avgnum = Math.Round(avgnum, 1);
                avgRatings.Add(avgnum);
            }

            // Create a dictionary for product and avg rating
            Dictionary<double, Product> pRatingLookup = new Dictionary<double, Product>();
            for (int i = 0; i < avgRatings.Count; i++)
            {
                Product p = new Product();
                p = db.Product.FirstOrDefault(x => x.Id == hasReview[i]);
                if(pRatingLookup.ContainsKey(avgRatings[i]) == false)
                {
                    pRatingLookup.Add(avgRatings[i], p);
                }
            }
            ViewData["PDictionary"] = pRatingLookup;

            // Get top 3 
            List<double> rvs = avgRatings.OrderByDescending(x => x).Distinct().ToList();
            List<double> TopReviews = new List<double>();
            for (int i = 0; i < 3; i++)
            {
                if (rvs.Count > i)
                    TopReviews.Add(rvs[i]);
            }
            ViewData["TopReviews"] = TopReviews;

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

            string sessionId = HttpContext.Session.GetString("SessionId");
            ViewData["SessionId"] = sessionId;

            // Display bubble using user's cart
            if (sessionId != null)
            {
                int userId = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId).UserId;
                Order order = db.Orders.FirstOrDefault(x => x.UserId == userId && x.IsPaid == false);
                if (order != null)
                {
                    List<Cart> cart = db.Cart.Where(x => x.OrderId == order.Id).ToList();
                    ViewData["Cart"] = cart;
                }
                else
                {
                    ViewData["Cart"] = null;
                }

                Session user = db.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
                ViewData["User"] = user;
            }
            // Display bubble using temp cart
            else
            {
                
                ViewData["User"] = null;

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

                if (tempCart.Count > 0)
                {
                    ViewData["Cart"] = tempCart;
                }
                else
                {
                    ViewData["Cart"] = null;
                }

            }

            return View();
        }
    }
}
