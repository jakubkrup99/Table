using Table.DataAccess.Db;
using Table.DataAccess.Repositories.Restaurant;

namespace Table.DataAccess.Repositories.UnitOfWork
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public IRestaurantRepository Restaurants { get; } = new RestaurantRepository(dbContext);
        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
