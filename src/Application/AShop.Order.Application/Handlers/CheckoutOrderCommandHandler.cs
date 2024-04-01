using AShop.Order.Application.Commands;
using AShop.Order.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AShop.Order.Application.Handlers;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand , long>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository , IMapper mapper , ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<long> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = this._mapper.Map<Domain.Entities.Order>(request);
        var generatedOrder = await this._orderRepository.AddAsync(orderEntity);
        _logger.LogInformation($"Order {generatedOrder} successfully created");
        return generatedOrder.Id;
    }
}