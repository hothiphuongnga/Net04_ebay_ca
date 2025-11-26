using AutoMapper;
using ebay.Application.DTOs;
using ebay.Domain.Entities;

public class ProductImageMapper : Profile
{
    public ProductImageMapper()
    {
        CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
    }
}