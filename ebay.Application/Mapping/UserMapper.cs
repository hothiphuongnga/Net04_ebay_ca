using AutoMapper;
using ebay.Application.DTOs;
using ebay.Domain.Entities;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UserRegisterDTO, User>()
        .ForMember(vm => vm.PasswordHash, opt => opt.Ignore())
        .ForMember(vm => vm.CreatedAt, opt => opt.MapFrom(dto => DateTime.Now));
    }
}