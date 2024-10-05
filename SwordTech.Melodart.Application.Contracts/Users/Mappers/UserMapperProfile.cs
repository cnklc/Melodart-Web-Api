using AutoMapper;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.User;

namespace SwordTech.Melodart.Application.Contract.Users.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => "images/"+src.ImageUrl));
            // .ForMember(dest => dest.Authorizations, opt => opt.MapFrom(src => ));
    }
}
