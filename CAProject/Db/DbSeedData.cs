using CAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

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
            AddActivationCodes();
            AddOrders();
        }

        protected void AddOrders()
        {
            db.Orders.Add(new Order
            {
                UserId = 2,
                OrderDate = DateTime.Now.ToString()
            });

            db.SaveChanges();

            db.Cart.Add(new Cart 
            {
                ProductId = 1,
                OrderId = 1,
                Quantity = 2
            });

            db.Cart.Add(new Cart
            {
                ProductId = 2,
                OrderId = 1,
                Quantity = 1
            });

            db.SaveChanges();

            db.Orders.Add(new Order
            {
                UserId = 1,
                OrderDate = DateTime.Now.ToString()
            });

            db.SaveChanges();

            db.Cart.Add(new Cart
            {
                ProductId = 2,
                OrderId = 2,
                Quantity = 1
            });

            db.Cart.Add(new Cart
            {
                ProductId = 3,
                OrderId = 2,
                Quantity = 1
            });

            db.SaveChanges();
        }

        protected void AddActivationCodes()
        {
            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "1234-5678-1234-5688",
                ProductId = 1,
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "2234-5678-1234-5688",
                ProductId = 1,
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "3234-5678-1234-5688",
                ProductId = 1,
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "4234-5678-1234-5688",
                ProductId = 1,
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "5234-5678-1234-5688",
                ProductId = 1,
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "6234-5678-1234-5688",
                ProductId = 1,
                IsSold = true
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "6234-5678-1234-5688",
                ProductId = 2,
            });

            db.ActivationCode.Add(new ActivationCode
            {
                ActivationCodeId = "6234-5678-1234-5688",
                ProductId = 3,
            });

            db.SaveChanges();
        }

        protected void AddProducts()
        {
            db.Product.Add(new Product
            {
                Name = "MacBook Master Race",
                Platform = "MacOS",
                Description = "MacOS is better than Windows",
                Image = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg",
                Price = 100,
            });

            db.Product.Add(new Product
            {
                Name = ".NET Core Entity Framework",
                Platform = "Windows 10",
                Description = "Helping to link the database to web solutions, and also the cause of 99% of my worries. Good for inducing stress when nothing works.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/1200px-.NET_Core_Logo.svg.png",
                Price = 100,
            }); 


            db.Product.Add(new Product
            {
                Name = ".NET Core",
                Platform = "Windows 10",
                Description = "A conducive app to build web applications. So renowned that all teachers use it, so it's the cause of stress for many students.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/1200px-.NET_Core_Logo.svg.png",
                Price = 100,
            }); ;

            for (int i = 0; i < 10; i++)
            {
                db.Product.Add(new Product
                {
                    Name = $"Product {i}",
                    Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    Image = "/NET_Logo.svg",
                    Price = 9.99,
                    Platform = "Windows 10",
                });
            }

            db.Product.Add(new Product
            {
                Name = "Troublesome Product",
                Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum tortor quam, feugiat vitae, ultricies eget, tempor sit amet, ante. Donec eu libero sit amet quam egestas semper. Aenean ultricies mi vitae est. Mauris placerat eleifend leo. Quisque sit amet est et sapien ullamcorper pharetra. Vestibulum erat wisi, condimentum sed, commodo vitae, ornare sit amet, wisi. Aenean fermentum, elit eget tincidunt condimentum, eros ipsum rutrum orci, sagittis tempus lacus enim ac dui. Donec non enim in turpis pulvinar facilisis. Ut felis. Praesent dapibus, neque id cursus faucibus, tortor neque egestas augue, eu vulputate magna eros eu erat. Aliquam erat volutpat. Nam dui mi, tincidunt quis, accumsan porttitor, facilisis luctus, metus",
                Image = "/NET_Logo.svg",
                Price = 9999,
                Platform = "Windows 10",
            });


            db.Product.Add(new Product
            {
                Name = "Another Troublesome Product",
                Description = "",
                Image = "/NET_Logo.svg",
                Price = 0,
                Platform = "Windows 10",
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
                    Password = BC.HashPassword(testers[i]),
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
                DateReviewed = "21/09/2020 11:36:23 am",
                Rating = 3,
                ProductId = 1,
                MainReview = "This program has caused me a lot of suffering."

            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "09/09/2020 9:17:54 am",
                Rating = 5,
                ProductId = 1,
                MainReview = "whewwhew avniafnifladjfsjlsjjisdfjsojdfsoijfsoidjfosfjoisdjofsdijo sdfio sdifjosdfj sdfioqdo aijvijne qwidjoc qqefow qwioqj dq qwoijfq jefew qwoeiq oajsfoas adijoadij saijaoisdja aosijcas asodij qwijd j"

            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "01/10/2020 10:55:54 am",
                Rating = 5,
                ProductId = 2,
                MainReview = "whewwhew kana kana"

            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "05/10/2020 10:31:04 am",
                Rating = 5,
                ProductId = 3,
                MainReview = "whewwhew kana kana"

            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "03/10/2020 11:59:20 am",
                Rating = 4,
                ProductId = 3,
                MainReview = "whewwhew kana kana"

            });

            db.SaveChanges();
        }
    
    }
}
