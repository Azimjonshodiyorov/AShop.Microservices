using AShop.Basket.Application.Responses;
using AShop.Basket.Domain.Entities;
using MediatR;

namespace AShop.Basket.Application.Commands;

public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> CartItems { get; set; }

    public CreateShoppingCartCommand(string userName , List<ShoppingCartItem> cartItems )
    {
        UserName = userName;
        CartItems = cartItems;
    }
}