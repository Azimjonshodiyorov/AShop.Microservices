using AShop.Order.Application.Queries;
using AShop.Order.Application.Responses;
using AShop.Order.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace AShop.Order.Application.Handlers;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery , List<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository orderRepository , IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await this._orderRepository.GetOrdersByUserName(request.UserName);
        var result = this._mapper.Map<List<OrderResponse>>(orderList);
        return result;
    }
}