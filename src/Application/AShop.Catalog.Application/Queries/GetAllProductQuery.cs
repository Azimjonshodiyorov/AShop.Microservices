using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Specs;
using MediatR;

namespace AShop.Catalog.Application.Queries;

public class GetAllProductQuery : IRequest<Pagination<ProductRespons>>
{
    public CatalogSpecsParams CatalogSpecsParams { get; set; }

    public GetAllProductQuery(CatalogSpecsParams catalogSpecsParams)
    {
        CatalogSpecsParams = catalogSpecsParams;
    }
}