using AutoMapper;
using SwordTech.Melodart.Application.Contract.Classes.Models;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.Classes;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.User;

namespace SwordTech.Melodart.Application.Contract.Base;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // ClassMapperProfile();
        // DepartmentMapperProfile();
        // UserMapperProfile();

    }

    // public void ClassMapperProfile()
    // {
    //     CreateMap<Class, ClassDto>();
    //     CreateMap<ClassCreateDto, Class>();
    //     CreateMap<ClassUpdateDto, Class>();
    // }
    //
    // public void DepartmentMapperProfile()
    // {
    //     CreateMap<Department, DepartmentDto>();
    //     CreateMap<DepartmentCreatDto, Department>();
    //     CreateMap<DepartmentUpdateDto, Department>();
    // }
    //
    // public void UserMapperProfile()
    // {
    //     CreateMap<AppUser, UserDto>()
    //         .ForMember(dest => dest.Authorizations, opt => opt.Ignore());
    //     ;
    // }

}
