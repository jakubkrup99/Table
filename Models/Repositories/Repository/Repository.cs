using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Table.DataAccess.Repositories.Repository
{
    public class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : class
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);    
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(TEntity entity)
        {
            var result = await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            var result = dbContext.Remove(entity);
        }
    }
}
