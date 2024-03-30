using AShop.Order.Infrastructure.OrderDbContext;
using AShop.Order.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AShop.Order.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Domain.Entities.Order> , IOrderRepository
{
    private readonly OrderContext _orderContext;

    public OrderRepository(OrderContext orderContext) : base(orderContext)
    {
        _orderContext = orderContext;
    }
    public async ValueTask<IEnumerable<Domain.Entities.Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await this._orderContext.Orders
            .Where(x => x.UserName == userName).ToListAsync();
        return orderList;
    }

}