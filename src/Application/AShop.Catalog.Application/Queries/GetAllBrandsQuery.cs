using AShop.Catalog.Application.Responses;
using MediatR;

namespace AShop.Catalog.Application.Queries;

public class GetAllBrandsQuery : IRequest<IList<BrandRespons>>
{
    
}   