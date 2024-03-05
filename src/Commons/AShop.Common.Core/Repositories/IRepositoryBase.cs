namespace AShop.Common.Core.Repositories;

public interface IRepositoryBase<T>
{
    ValueTask<IQueryable<T>> GetAllAsync();
    ValueTask<T> GetByIdAsync(string id);
    ValueTask<T> PostAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
}