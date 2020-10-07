using CAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Db
{
    public class DbSeedData
    {
        private readonly DbGallery db;

        public DbSeedData(DbGallery db)
        {
            this.db = db;
        }

        public void Init()
        {
            AddProducts();
            AddUsers();
            AddReviews();
        }

        protected void AddProducts()
        {
            db.Product.Add(new Product
            {
                Name = ".NET Core Entity Framework",
                Platform = "Windows 10",
                Ratings = 4.2,
                NumberRatings = 1000,
                NumberSold = 10,
                Description = "Helping to link the database to web solutions, and also the cause of 99% of my worries. Good for inducing stress when nothing works.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/1200px-.NET_Core_Logo.svg.png",
                Price = 100,
                StockCount = 10
            }); ;


            db.Product.Add(new Product
            {
                Name = ".NET Core",
                Platform = "Windows 10",
                Ratings = 4.5,
                NumberRatings = 1000,
                NumberSold = 10,
                Description = "A conducive app to build web applications. So renowned that all teachers use it, so it's the cause of stress for many students.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/1200px-.NET_Core_Logo.svg.png",
                Price = 100,
                StockCount = 0
            }); ;

            for (int i = 0; i < 34; i++)
            {
                db.Product.Add(new Product
                {
                    Name = $"Product {i}",
                    Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    Image = "/NET_Logo.svg",
                    Price = 9.99,
                    StockCount = 10,
                    Platform = "Windows 10",
                    NumberSold = 20,
                    NumberRatings = 10,
                    Ratings = 4.3
                });
            }

            db.Product.Add(new Product
            {
                Name = "Troublesome Product",
                Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum tortor quam, feugiat vitae, ultricies eget, tempor sit amet, ante. Donec eu libero sit amet quam egestas semper. Aenean ultricies mi vitae est. Mauris placerat eleifend leo. Quisque sit amet est et sapien ullamcorper pharetra. Vestibulum erat wisi, condimentum sed, commodo vitae, ornare sit amet, wisi. Aenean fermentum, elit eget tincidunt condimentum, eros ipsum rutrum orci, sagittis tempus lacus enim ac dui. Donec non enim in turpis pulvinar facilisis. Ut felis. Praesent dapibus, neque id cursus faucibus, tortor neque egestas augue, eu vulputate magna eros eu erat. Aliquam erat volutpat. Nam dui mi, tincidunt quis, accumsan porttitor, facilisis luctus, metus",
                Image = "/NET_Logo.svg",
                Price = 9999,
                StockCount = 10,
                Platform = "Windows 10",
                NumberSold = 20,
                NumberRatings = 10,
                Ratings = 1.2
            });


            db.Product.Add(new Product
            {
                Name = "Another Troublesome Product",
                Description = "",
                Image = "/NET_Logo.svg",
                Price = 0,
                StockCount = 10,
                Platform = "Windows 10",
                NumberSold = 20,
                NumberRatings = 10,
                Ratings = 2.3
            });

            db.SaveChanges();
        }

        protected void AddUsers()
        {
            string[] testers = { "Alice", "Brian" };

            for (int i = 0; i < testers.Length; i++)
            {
                db.Users.Add(new User()
                {
                    Email = testers[i] + "@test.com",
                    Password = testers[i],
                    Name = testers[i]
                });
            }
            db.SaveChanges();
        }

        protected void AddReviews()
        {
            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "19 October 2019",
                Rating = 3,
                ProductId = 1,
                MainReview = "This program has caused me a lot of suffering."

            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "21 October 2019",
                Rating = 5,
                ProductId = 1,
                MainReview = "whewwhew avniafnifladjfsjlsjjisdfjsojdfsoijfsoidjfosfjoisdjofsdijo sdfio sdifjosdfj sdfioqdo aijvijne qwidjoc qqefow qwioqj dq qwoijfq jefew qwoeiq oajsfoas adijoadij saijaoisdja aosijcas asodij qwijd j"

            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "21 October 2019",
                Rating = 5,
                ProductId = 2,
                MainReview = "whewwhew kana kana"

            });

            db.SaveChanges();
        }
    
    }
}
