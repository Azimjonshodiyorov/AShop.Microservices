using AShop.Catalog.Application.Responses;
using MediatR;

namespace AShop.Catalog.Application.Queries;

public class GetProductByNameQuery : IRequest<IList<ProductRespons>>
{
    public string Name { get; set; }

    public GetProductByNameQuery(string name)
    {
        Name = name;
    }
}