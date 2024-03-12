using MediatR;

namespace AShop.Discount.Application.Commands;

public class DeleteDiscountCommand  : IRequest<bool>
{
    public string ProductName { get; set; }

    public DeleteDiscountCommand(string productName)
    {
        ProductName = productName;
    }
}