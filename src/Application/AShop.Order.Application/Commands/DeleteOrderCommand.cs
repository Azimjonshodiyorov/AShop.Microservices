using MediatR;

namespace AShop.Order.Application.Commands;

public class DeleteOrderCommand : IRequest
{
    public long Id { get; set; }
}