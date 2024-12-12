using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table.DataAccess.Db;
using Table.DataAccess.Models;

namespace Table.DataAccess.Seed
{
    internal class RestaurantSeeder(ApplicationDbContext dbContext) : IRestaurantSeeder
    {
        public async Task SeedAsync()
        {
            if (!await dbContext.Database.CanConnectAsync())
                return;

            if (dbContext.Restaurants.Any())
                return;

            var restaurants = GetRestaurants();
            dbContext.Restaurants.AddRange(restaurants);
            await dbContext.SaveChangesAsync();
        }

        private List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new()
            {
                new Restaurant
                {
                    Name = "Manekin",
                    Address = "Rynek Staromiejski",
                    City = "Toruń",
                    PhoneNumber = "1234567890",
                    Email = "Manekin@wp.pl",
                    ImageUrl = "empty",
                    OpeningHour = TimeOnly.Parse("6:00"),
                    ClosingHour = TimeOnly.Parse("23:00")

                }
            };

            return restaurants;
        }
    }
}
