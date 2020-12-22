using CAProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
            // AddOrders();
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
            for(int i = 1; i <= 13; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    db.ActivationCode.Add(new ActivationCode
                    {
                        ActivationCodeId = (Convert.ToString(j)) + "234-5678-1234-5688",
                        ProductId = i,
                    });
                }
            }

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
            });

            db.Product.Add(new Product
            {
                Name = "Gdipsa simulator",
                Description = "The most true to life GDIPSA simulator on the planet",
                Image = "https://i.imgur.com/pIJlu1x.png",
                Price = 15000,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Clip Studio Paint",
                Description = "Clip Studio Paint was made for creators who love to draw and paint. With a natural brush feel beyond other graphics software, you can enjoy creating your vision just how you want it.",
                Image = "https://is1-ssl.mzstatic.com/image/thumb/Purple113/v4/7f/77/2d/7f772d1d-ca60-11d3-f2aa-1d6fa0371a9e/AppIcon-0-0-1x_U007emarketing-0-0-0-6-0-0-sRGB-0-0-0-GLES2_U002c0-512MB-85-220-0-0.png/246x0w.png",
                Price = 325.17,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "TypeScript",
                Description = "TypeScript was such a boon to our stability and sanity that we started using it for all new code within days of starting the conversion. Felix Rieseberg at Slack covered the transition of their desktop app from JavaScript to TypeScript in their blog",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Typescript_logo_2020.svg/800px-Typescript_logo_2020.svg.png",
                Price = 11.11,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Fall Guys: Ultimate Knockout",
                Description = "Fall Guys is a massively multiplayer party game with up to 60 players online in a free-for-all struggle through round after round of escalating chaos until one victor remains!",
                Image = "https://www.mobygames.com/images/covers/l/676144-fall-guys-ultimate-knockout-playstation-4-front-cover.png",
                Price = 20,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Call of Duty: Warzone",
                Description = "Call of Duty: Warzon allows online muliplayers combat among 150 players. Developed by Infinity Ward and Raven Software and published by Activision.",
                Image = "https://upload.wikimedia.org/wikipedia/en/7/71/COD_Warzone_cover_art.jpg",
                Price = 60,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "WinRAR",
                Description = "If you pay for this. You are an idiot.",
                Image = "https://hips.hearstapps.com/pop.h-cdn.co/assets/17/11/768x768/square-1489520090-winrar.png?resize=980:*",
                Price = 999,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Adobe Photoshop",
                Description = "The world’s best imaging and graphic design software is at the core of just about every creative project, from photo editing and compositing to digital painting, animation, and graphic design. And now you can harness the power of Photoshop across desktop and iPad to create wherever inspiration strikes.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/af/Adobe_Photoshop_CC_icon.svg/1200px-Adobe_Photoshop_CC_icon.svg.png",
                Price = 315.61,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Parallels",
                Description = "Parallels Inc., a global leader in cross-platform solutions, makes it simple for customers to use and access the applications and files they need on any device or operating system.",
                Image = "https://upload.wikimedia.org/wikipedia/en/0/03/Parallelsdesktop.png",
                Price = 20,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Norton Antivirus",
                Description = "Real-time Threat Protection, Password Manager, Firewall for PC or Mac® & More. Enjoy Multi-Device Security, Secure VPN, Password Manager, PC Cloud Backup and More.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Norton_av_logo.png",
                Price = 1,
                Platform = "",
            });

            db.Product.Add(new Product
            {
                Name = "Adobe Animate",
                Description = " Animate just about anything, get your game on, create characters that come alive, publish to any platform.",
                Image = "https://cdn.worldvectorlogo.com/logos/adobe-animate.svg",
                Price = 270,
                Platform = "",
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
                UserName = "Brian",
                DateReviewed = "21/09/2020 11:36:23 am",
                Rating = 5,
                ProductId = 1,
                MainReview = "The superior option."

            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "21/09/2020 11:36:23 am",
                Rating = 3,
                ProductId = 2,
                MainReview = "This program has caused me a lot of suffering."

            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "21/09/2020 11:36:23 am",
                Rating = 3,
                ProductId = 3,
                MainReview = "I get trauma when I look at it now. :( "
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "21/09/2020 11:36:23 am",
                Rating = 5,
                ProductId = 3,
                MainReview = "Great tool to use when learning how to code!!!"
            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "20/09/2020 11:36:23 am",
                Rating = 4,
                ProductId = 4,
                MainReview = "Very real graphics and very good quality lectures"
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "21/08/2020 11:36:23 am",
                Rating = 1,
                ProductId = 4,
                MainReview = "Game crashes too often, i want a refund"
            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "10/09/2020 11:36:23 am",
                Rating = 5,
                ProductId = 5,
                MainReview = "Flexible art creation platform that makes it easy for artists to create new tools and share them with other creators around the world. The EX version can even let me make animations, which is impressive! Best thing of all - even though it is expensive, I only have to pay once to get a life-time subscription to this product. "
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "21/07/2020 11:36:23 am",
                Rating = 5,
                ProductId = 5,
                MainReview = "Best product ever! Highly recommend!!!"
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "21/01/2020 11:36:23 am",
                Rating = 3,
                ProductId = 6,
                MainReview = "Typescript has optional static typing with support for interfaces and generics, and intelligent type inference."
            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "12/08/2020 11:36:23 am",
                Rating = 5,
                ProductId = 7,
                MainReview = "Knocks my breath away!"
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "12/08/2020 11:36:23 am",
                Rating = 5,
                ProductId = 8,
                MainReview = "It's a masterpiece!! Definitely worth it"
            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "24/08/2020 11:36:23 am",
                Rating = 4,
                ProductId = 9,
                MainReview = "Is a full featured program, very intuitive to use, ideal to create .rar (and .zip) archives, it has a very good extraction speed, but lacks at compression formats and compression speed."
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "12/08/2020 11:36:23 am",
                Rating = 5,
                ProductId = 10,
                MainReview = "I can edit my photos with this easily! Loving it! And the best part is that because so many people use it, there are many tutorials for it online."
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "30/09/2020 11:36:23 am",
                Rating = 1,
                ProductId = 11,
                MainReview = "Don't know how to use."
            });

            db.Review.Add(new Review
            {
                UserName = "Brian",
                DateReviewed = "20/09/2020 11:36:23 am",
                Rating = 2,
                ProductId = 12,
                MainReview = "I did not want to use it because I was given a free subscription for another anti-virus software. But Norton still keeps showing me pop-ups everyday to get me to subscribe to their service."
            });

            db.Review.Add(new Review
            {
                UserName = "Alice",
                DateReviewed = "30/09/2020 11:36:23 am",
                Rating = 4,
                ProductId = 12,
                MainReview = "Fun and easy to use! I am having a great time with it! If only I could buy the product one-off instead of having to pay monthly for it though..."
            });


            db.SaveChanges();
        }
    
    }
}
