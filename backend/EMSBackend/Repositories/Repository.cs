using EMSBackend.Data;
using EMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMSBackend.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> dbSet;
        private readonly ApplicationDBContext _dBContext;

        public Repository(ApplicationDBContext dBContext)
        {
            dbSet = dBContext.Set<T>();
            _dBContext = dBContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var list = await dbSet.ToListAsync();
            return list;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }


        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity!);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
