using eCommerce.Core.Entities;
using System.Linq.Expressions;

namespace eCommerce.Data.Repository.Abstractions
{
    public interface IRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T Entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByGuidAsync(Guid guid);
        Task<T> UpdateAsync(T Entity);
        Task DeleteAsync(T Entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
