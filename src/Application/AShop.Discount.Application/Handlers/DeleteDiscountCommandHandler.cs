using AShop.Discount.Application.Commands;
using AShop.Discount.Infrastructure.Repositories;
using AutoMapper;
using MediatR;

namespace AShop.Discount.Application.Handlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand , bool>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository , IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var delete = await this._discountRepository.DeleteDiscount(request.ProductName);
        return delete;
    }
}