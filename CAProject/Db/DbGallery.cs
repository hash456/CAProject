using CAProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CAProject.Db
{
    public class DbGallery : DbContext
    {
        public DbGallery(DbContextOptions<DbGallery> options)
            : base(options)
        {
            // options like which database provider to use (e.g.
            // MS SQL, Oracle, SQL Lite, MySQL
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // to customize tables (e.g. set a rule to enforce that
            // values within a column have to be unqiue)


            // Create a composite key in ActivationCode table
            // Note: .HasCompositeKey does not make our composite key the primary key, need to use .HasKey to do this
            model.Entity<ActivationCode>().HasKey(x => new { x.ActivationCodeId, x.ProductId });
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Review> Review { get; set; }
        public DbSet<ActivationCode> ActivationCode { get; set; }
    }
}
