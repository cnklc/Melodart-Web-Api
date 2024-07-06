using AutoMapper;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Domain.User;

namespace SwordTech.Melodart.Application.Mapper;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<AppUser, UserDto>();
    }
}
