using AShop.Basket.Application.Responses;
using MediatR;

namespace AShop.Basket.Application.Queries;

public class GetBasketByUserNameQuery : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; }

    public GetBasketByUserNameQuery(string userName)
    {
        UserName = userName;
    }
}