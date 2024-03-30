using System.Linq.Expressions;
using AShop.Order.Domain.Common;
using AShop.Order.Infrastructure.OrderDbContext;
using AShop.Order.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AShop.Order.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    private readonly OrderContext _orderContext;

    public RepositoryBase(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }
    public async ValueTask<IReadOnlyList<T>> GetAllAsync()
    {
        return await this._orderContext.Set<T>().ToListAsync();
    }

    public async ValueTask<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await this._orderContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async ValueTask<T> GetByIdAsync(long id)
    {
        return await this._orderContext.Set<T>().FindAsync(id);
    }

    public async ValueTask<T> AddAsync(T entity)
    {
        await this._orderContext.AddAsync(entity);
        await this._orderContext.SaveChangesAsync();
        return entity;
    }

    public async ValueTask<T> UpdateAsync(T entity)
    {
        this._orderContext.Entry(entity).State = EntityState.Modified;
        await this._orderContext.SaveChangesAsync();
        return entity;
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        this._orderContext.Remove(entity);
        await this._orderContext.SaveChangesAsync();
        return entity;
    }
}