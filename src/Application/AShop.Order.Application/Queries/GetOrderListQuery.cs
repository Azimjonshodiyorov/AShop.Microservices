using AShop.Order.Application.Responses;
using MediatR;

namespace AShop.Order.Application.Queries;

public class GetOrderListQuery : IRequest<List<OrderResponse>>
{
    public string UserName { get; set; }

    public GetOrderListQuery(string userName)
    {
        UserName = userName;
    }
}