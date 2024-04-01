using AShop.Order.Application.Commands;
using AShop.Order.Application.Excaptions;
using AShop.Order.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AShop.Order.Application.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository , IMapper mapper , ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await this._orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate is null)
            throw new OrderNotFoundExcaption(nameof(Domain.Entities.Order), request.Id);
        this._mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Domain.Entities.Order));
        await this._orderRepository.UpdateAsync(orderToUpdate);
        return Unit.Value;
    }
}