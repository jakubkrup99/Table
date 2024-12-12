
using Table.DataAccess.Repositories.Restaurant;

namespace Table.DataAccess.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRestaurantRepository Restaurants { get; }
        Task SaveAsync();
    }
}
