using AShop.Discount.Application.Queries;
using AShop.Discount.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace AShop.Discount.Application.Handlers;

public class GetDiscountCommandHandler  : IRequestHandler<GetDiscountQuery , CouponModel>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await this._discountRepository.GetDiscount(request.ProductName);
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with the ProductName {request.ProductName} Not Found"));
        }
        
        return new CouponModel()
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount,
        };
    }
}