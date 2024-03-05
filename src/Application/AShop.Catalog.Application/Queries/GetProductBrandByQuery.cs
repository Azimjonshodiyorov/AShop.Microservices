using AShop.Catalog.Application.Responses;
using MediatR;

namespace AShop.Catalog.Application.Queries;

public class GetProductBrandByQuery : IRequest<IList<ProductRespons>>
{
    public string BrandName { get; set; }

    public GetProductBrandByQuery(string brandName)
    {
        BrandName = brandName;
    }
}