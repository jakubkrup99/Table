
using Table.DataAccess.Models;
using Table.DataAccess.Repositories.Repository;

namespace Table.DataAccess.Repositories.Restaurant
{
    public interface IRestaurantRepository : IRepository<Models.Restaurant>
    {
        public void Update(Models.Restaurant restaurant);
    }
}
