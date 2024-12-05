
using Microsoft.EntityFrameworkCore;
using Table.DataAccess.Db;
using Table.DataAccess.Models;
using Table.DataAccess.Repositories.Repository;

namespace Table.DataAccess.Repositories.Restaurant
{
    public class RestaurantRepository(ApplicationDbContext dbContext) : Repository<Models.Restaurant>(dbContext), IRestaurantRepository
    {

        public void Update(Models.Restaurant restaurant)
        {
            dbContext.Restaurants.Update(restaurant);
        }
    }
}
