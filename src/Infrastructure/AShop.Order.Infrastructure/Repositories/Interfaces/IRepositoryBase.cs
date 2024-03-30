using System.Linq.Expressions;
using AShop.Order.Domain.Common;

namespace AShop.Order.Infrastructure.Repositories.Interfaces;

public interface IRepositoryBase<T> where T : BaseEntity
{
    ValueTask<IReadOnlyList<T>> GetAllAsync();
    ValueTask<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    ValueTask<T> GetByIdAsync(long id);
    ValueTask<T> AddAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
}