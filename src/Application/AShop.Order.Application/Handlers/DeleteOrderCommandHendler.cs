using AShop.Order.Application.Commands;
using AShop.Order.Application.Excaptions;
using AShop.Order.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AShop.Order.Application.Handlers;

public class DeleteOrderCommandHendler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<DeleteOrderCommandHendler> _logger;

    public DeleteOrderCommandHendler(IOrderRepository orderRepository , ILogger<DeleteOrderCommandHendler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await this._orderRepository.GetByIdAsync(request.Id);
        if (orderToDelete is null)
            throw new OrderNotFoundExcaption(nameof(Domain.Entities.Order), request.Id);
        await this._orderRepository.DeleteAsync(orderToDelete);
        _logger.LogInformation($"Order with id {request.Id} is deleted successfully");
        return Unit.Value;
    }
}