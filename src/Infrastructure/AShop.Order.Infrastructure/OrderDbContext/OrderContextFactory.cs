using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AShop.Order.Infrastructure.OrderDbContext;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsDb = new DbContextOptionsBuilder<OrderContext>();
        optionsDb.UseSqlServer("Data Source=OrderDb");
        return new OrderContext(optionsDb.Options);
    }
}