
using Discount.Grpc.Protos;

namespace AShop.Basket.Application.GrpcService;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _protoServiceClient;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient protoServiceClient  )
    {
        _protoServiceClient = protoServiceClient;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequst = new GetDiscountRequest() { ProductName = productName };
        return await this._protoServiceClient.GetDiscountAsync(discountRequst);
    }
}