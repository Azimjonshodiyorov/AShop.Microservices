﻿using AShop.Discount.Application.Commands;
using AShop.Discount.Domain.Entities;
using AShop.Discount.Infrastructure.Repositories;
using AutoMapper;
using Discount.Grpc.Protos;
using MediatR;

namespace AShop.Discount.Application.Handlers;

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand , CouponModel>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public UpdateDiscountCommandHandler(IDiscountRepository discountRepository , IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }
    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = this._mapper.Map<Coupon>(request);
        await this._discountRepository.UpdateDiscount(coupon);
        var result = this._mapper.Map<CouponModel>(coupon);
        return result;
    }
}