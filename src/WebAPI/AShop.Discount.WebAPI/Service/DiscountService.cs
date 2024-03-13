using AShop.Common.Logging.Correlation;
using AShop.Discount.Application.Commands;
using AShop.Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace AShop.Discount.WebAPI.Service;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiscountService> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;


    public DiscountService(IMediator mediator , ILogger<DiscountService> logger , ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }


    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new CreateDiscountCommand()
        {
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount,
        };
        var result = await this._mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully created for the Product Name: {result.ProductName}");
        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var cmd = new DeleteDiscountCommand(request.ProductName);
        var delete = await this._mediator.Send(cmd);
        var response = new DeleteDiscountResponse()
        {
            Success = delete
        };
        return response;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var cmd = new GetDiscountQuery(request.ProductName);
        var result = await this._mediator.Send(cmd);
        _logger.LogInformation(
            $"Discount is retrieved for the Product Name: {request.ProductName} and Amount : {result.Amount}");
        return result;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new UpdateDiscountCommand()
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount,
        };
        var result = await this._mediator.Send(cmd);
        _logger.LogInformation($"Discount is successfully updated Product Name: {result.ProductName}");
        return result;
    }
}