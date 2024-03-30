using Microsoft.Extensions.Logging;

namespace AShop.Order.Infrastructure.OrderDbContext;

public class OrderContexSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContexSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded.");
        }
    }

    private static IEnumerable<Domain.Entities.Order> GetOrders()
    {
        return new List<Domain.Entities.Order>
        {
            new()
            {
                UserName = "azimjon",
                FirstName = "Azimjon",
                LastName = "Shodiyorov",
                EmailAddress = "azimjonshodiyorov@gmail.com",
                AddressLine = "Yunsabod",
                Country = "Toshkent",
                TotalPrice = 750,
                State = "KA",
                ZipCode = "560001",

                CardName = "Visa",
                CardNumber = "1234567890123456",
                CreatedBy = "Azimjon",
                Expiration = "12/25",
                Cvv = "123",
                PaymentMethod = 1,
                LastModifiedBy = "Azimjon",
                LastModifiedDate = new DateTime(),
            }
        };
    }
}