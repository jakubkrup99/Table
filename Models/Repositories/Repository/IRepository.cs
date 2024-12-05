using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace Table.DataAccess.Repositories.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity?> GetAsync(int id);

        public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);

        public Task AddAsync(TEntity entity);

        public void Delete(TEntity entity);

    }
}
