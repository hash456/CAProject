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
        }

        protected void AddProducts()
        {
            for (int i = 0; i < 34; i++)
            {
                db.Product.Add(new Product
                {
                    Name = $"Product {i}",
                    Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    Image = "/NET_Logo.svg",
                    Price = 9.99,
                    StockCount = 10
                });
            }

            db.Product.Add(new Product
            {
                Name = "Troublesome Product",
                Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum tortor quam, feugiat vitae, ultricies eget, tempor sit amet, ante. Donec eu libero sit amet quam egestas semper. Aenean ultricies mi vitae est. Mauris placerat eleifend leo. Quisque sit amet est et sapien ullamcorper pharetra. Vestibulum erat wisi, condimentum sed, commodo vitae, ornare sit amet, wisi. Aenean fermentum, elit eget tincidunt condimentum, eros ipsum rutrum orci, sagittis tempus lacus enim ac dui. Donec non enim in turpis pulvinar facilisis. Ut felis. Praesent dapibus, neque id cursus faucibus, tortor neque egestas augue, eu vulputate magna eros eu erat. Aliquam erat volutpat. Nam dui mi, tincidunt quis, accumsan porttitor, facilisis luctus, metus",
                Image = "/NET_Logo.svg",
                Price = 9999,
                StockCount = 10
            });


            db.Product.Add(new Product
            {
                Name = "Another Troublesome Product",
                Description = "",
                Image = "/NET_Logo.svg",
                Price = 0,
                StockCount = 10
            });

            db.SaveChanges();
        }
    }
}
