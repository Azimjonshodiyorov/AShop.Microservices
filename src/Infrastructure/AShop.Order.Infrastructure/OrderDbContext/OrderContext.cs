using AShop.Order.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AShop.Order.Infrastructure.OrderDbContext;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added :
                    entry.Entity.CreateDate = DateTime.Now;
                    entry.Entity.CreatedBy = "Azimjon";
                    break;
                case EntityState.Modified :
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "Azimjon";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}