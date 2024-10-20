using AutoMapper;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Lessons;

namespace SwordTech.Melodart.Application.Contract.Lessons.Mappers;

public class ScheduleMapperProfile : Profile
{
    public ScheduleMapperProfile()
    {
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name + " " + src.Student.LastName))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name + " " + src.Teacher.LastName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

        CreateMap<ScheduleCreateDto, Schedule>();
        CreateMap<ScheduleUpdateDto, Schedule>();
    }
}
