using AShop.Catalog.Application.Commands;
using AShop.Catalog.Application.Responses;
using AShop.Catalog.Domain.Entities;
using AShop.Catalog.Domain.Specs;
using AutoMapper;

namespace AShop.Catalog.Application.Mappers;

public  class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductRespons>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<ProductBrand, BrandRespons>().ReverseMap();
        CreateMap<ProductType, TypeRespons>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductRespons>>().ReverseMap();
    }
}