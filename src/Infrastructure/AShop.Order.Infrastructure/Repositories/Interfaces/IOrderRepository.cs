using AShop.Order.Domain.Entities;
namespace AShop.Order.Infrastructure.Repositories.Interfaces;

public interface IOrderRepository : IRepositoryBase<Domain.Entities.Order>
{
    ValueTask<IEnumerable<Domain.Entities.Order>> GetOrdersByUserName(string userName);
}