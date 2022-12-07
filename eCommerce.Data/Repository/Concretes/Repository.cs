using eCommerce.Core.Entities;
using eCommerce.Data.Context;
using eCommerce.Data.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Data.Repository.Concretes
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext context;
        private DbSet<T> Table { get => context.Set<T>(); }

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T Entity)
        {
            await Table.AddAsync(Entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Table.CountAsync(predicate);
        }

        public async Task DeleteAsync(T Entity)
        {
            await Task.Run(() =>
            {
                Table.Remove(Entity);
            });
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);
            if (includeProperties.Any())
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            return await query.SingleAsync();
        }

        public async Task<T> GetByGuidAsync(Guid guid)
        {
            return await Table.FindAsync(guid);
        }

        public async Task<T> UpdateAsync(T Entity)
        {
            await Task.Run(() =>
            {
                Table.Update(Entity);
            });
            return Entity;
        }
    }
}
