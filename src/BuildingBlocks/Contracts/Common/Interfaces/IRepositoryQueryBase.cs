using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Common.Interfaces
{
    public interface IRepositoryQueryBase<T, K, TContext> where T: EntityBase<K>
        where TContext : DbContext
    {
        IQueryable<T> FindAll(bool trackChanges = false);

        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetByIdAsync(K id);

        Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    }

    public interface IRepsitoryBaseAsync<T, K, TContext> : IRepositoryQueryBase<T, K, TContext> where T: EntityBase<K>
        where TContext : DbContext
    {
        Task<K> CreateAsync(T entity);
        Task<List<T>> CreateListAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UploadListAsync(IEnumerable<T> entities);
        Task DeleteAsync(T Entity);
        Task<List<T>> DeleteListAsync(IEnumerable<T> entities);
        Task<int> SaveChangeAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task EndTransactionAsync();
        Task RollbackTranaction();
    }
}
