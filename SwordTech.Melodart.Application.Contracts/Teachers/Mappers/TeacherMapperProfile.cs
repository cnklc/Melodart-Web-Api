using AutoMapper;
using SwordTech.Melodart.Application.Contract.Teachers.Models; 
using SwordTech.Melodart.Domain.Teachers;

namespace SwordTech.Melodart.Application.Contract.Teachers.Mappers;

public class TeacherMapperProfile : Profile
{
    public TeacherMapperProfile()
    {
        CreateMap<Teacher, TeacherDto>()
            .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.TeacherDepartments.Select(x=>x.Department)));
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<TeacherUpdateDto, Teacher>();
    }
}
