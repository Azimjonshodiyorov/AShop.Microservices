using AShop.Catalog.Application.Mappers;
using AShop.Catalog.Application.Queries;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace AShop.Catalog.Application.Handlers;

public class GetAllTypesHandler : IRequestHandler<GetAllTypeQuery , IList<TypeRespons>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypesHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }
    public async Task<IList<TypeRespons>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
    {
        var typeList = await this._typeRepository.GetAllTypes();
        var typeResponseList = ProductMapper.Mapper.Map<IList<TypeRespons>>(typeList);
        return typeResponseList;
    }
}