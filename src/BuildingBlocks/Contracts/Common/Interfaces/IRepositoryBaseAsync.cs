using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Contracts.Common.Interfaces
{
    public interface IRepositoryQueryBase<T, K> where T : EntityBase<K>
    {
        IQueryable<T> FindAll(bool trackChanges = false);

        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T?> GetByIdAsync(K id);

        Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    }

    public interface IRepositoryBaseAsync<T, K> : IRepositoryQueryBase<T, K> where T : EntityBase<K>
    {
        void Create(T entity);
        Task<T> CreateAsync(T entity);

        void CreateList(IEnumerable<T> entities);
        Task<IList<K>> CreateListAsync(IEnumerable<T> entities);

        void Update(T entity);
        Task<T> UpdateAsync(T entity);

        void UpdateLists(IEnumerable<T> entities);
        Task UpdateListAsync(IEnumerable<T> entities);

        void Delete(T entity);
        Task DeleteAsync(T entity);

        void DeleteList(IEnumerable<T> entities);
        Task DeleteListAsync(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();
    }

    public interface IRepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K> where T : EntityBase<K>
        where TContext : DbContext
    {
    }

    public interface IRepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K> where T : EntityBase<K>
        where TContext : DbContext
    {
    }
}