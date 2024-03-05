using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Entities;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery , IList<BrandRespons>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task<IList<BrandRespons>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await this._brandRepository.GetAllBrands();
        var brandResponsList = ProductMapper.Mapper.Map<IList<ProductBrand>, IList<BrandRespons>>(brandList.ToList());
        return brandResponsList;
    }
}