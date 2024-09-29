using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> Get(Expression<Func<T, bool>> exp);

        Task<bool> Delete(Expression<Func<T, bool>> exp);


    }
}
