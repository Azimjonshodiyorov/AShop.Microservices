using System.Linq.Expressions;
using AShop.Order.Domain.Common;
using AShop.Order.Infrastructure.Repositories.Interfaces;

namespace AShop.Order.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    public async ValueTask<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}