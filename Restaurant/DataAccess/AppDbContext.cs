using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DB.db;");
        }
    }
}
