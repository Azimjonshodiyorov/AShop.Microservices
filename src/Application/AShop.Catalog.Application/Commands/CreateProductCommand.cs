using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Entities;
using MediatR;

namespace AShop.Catalog.Application.Commands;

public class CreateProductCommand :IRequest<ProductRespons>
{
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    public ProductBrand Brands { get; set; }
    public ProductType Types { get; set; }
}