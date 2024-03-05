using AShop.Catalog.Application.Responses;
using MediatR;

namespace AShop.Catalog.Application.Queries;

public class GetProductByIdQuery : IRequest<ProductRespons>
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}