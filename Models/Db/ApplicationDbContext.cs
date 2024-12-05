using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table.DataAccess.Configuration;
using Table.DataAccess.Models;

namespace Table.DataAccess.Db
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Restaurant> Restaurants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<Restaurant>(new RestaurantConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
