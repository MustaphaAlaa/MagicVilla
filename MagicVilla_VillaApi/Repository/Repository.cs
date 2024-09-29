using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        VillaDbContext _db;
        DbSet<T> _dbSet;
        public Repository(VillaDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> exp)
        {

            var entity = await _dbSet.FirstOrDefaultAsync(exp);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;

        }

        public async Task<T?> Get(Expression<Func<T, bool>> exp)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(exp);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
