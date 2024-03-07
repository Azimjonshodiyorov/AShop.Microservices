using AShop.Basket.Domain.Entities;

namespace AShop.Basket.Infrastructure.Repositories;

public interface IBasketRepository
{
    ValueTask<ShoppingCart> GetBasket(string userName);
    ValueTask<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
    ValueTask DeleteBasket(string userName);
}