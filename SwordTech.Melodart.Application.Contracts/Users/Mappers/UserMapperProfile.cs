using AutoMapper;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.User;

namespace SwordTech.Melodart.Application.Contract.Users.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.Authorizations, opt => opt.Ignore());;
    }
}
