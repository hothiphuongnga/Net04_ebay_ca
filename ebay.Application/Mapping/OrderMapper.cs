using AutoMapper;
using ebay.Application.DTOs;
using ebay.Domain.Entities;
namespace ebay.Mapping;
public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderDto>().ReverseMap();

        CreateMap<CreateOrderDto, Order>()
        .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(dto => DateTime.Now));
        
        CreateMap<CreateOrderDto, OrderDetail>();
    }
}