using AShop.Discount.Domain.Entities;

namespace AShop.Discount.Infrastructure.Repositories;

public interface IDiscountRepository
{
    ValueTask<Coupon> GetDiscount(string productName);
    ValueTask<bool> CreateDiscount(Coupon coupon);
    ValueTask<bool> UpdateDiscount(Coupon coupon);
    ValueTask<bool> DeleteDiscount(string productName);
}