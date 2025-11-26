using AutoMapper;
using ebay.Application.DTOs;
using ebay.Domain.Entities;
public class RatingMapper : Profile
{
    public RatingMapper()
    {
        CreateMap<Rating, RatingDTO>().ReverseMap();
    }
}