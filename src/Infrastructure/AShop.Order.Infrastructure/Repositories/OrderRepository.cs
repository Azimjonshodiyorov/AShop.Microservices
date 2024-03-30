using AShop.Order.Infrastructure.Repositories.Interfaces;

namespace AShop.Order.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Domain.Entities.Order> , IOrderRepository
{
    public async ValueTask<IEnumerable<Domain.Entities.Order>> GetOrdersByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}