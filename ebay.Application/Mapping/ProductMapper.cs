using AutoMapper;
using ebay.Application.DTOs;
using ebay.Domain.Entities;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, ProductCreateDTO>().ReverseMap();
    }
}