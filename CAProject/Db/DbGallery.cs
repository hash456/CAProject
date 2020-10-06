using CAProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
