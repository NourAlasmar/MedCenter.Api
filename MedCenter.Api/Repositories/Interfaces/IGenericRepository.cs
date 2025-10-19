using System.Linq.Expressions;
using MedCenter.Api.Models;

namespace MedCenter.Api.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      string includeProperties = "",
                                      int? take = null,
                                      int? skip = null,
                                      CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

        Task AddAsync(T entity, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        void Update(T entity);
        void Remove(T entity);
        void SoftDelete(T entity, long? userId = null);
    }
}