using AutoMapper;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Domain.Contracts.Student;
using SwordTech.Melodart.Domain.Students;

namespace SwordTech.Melodart.Application.Contract.Students.Mappers;

public class StudentMapperProfile : Profile
{
    public StudentMapperProfile()
    {
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.TeacherStudents.Select(x => x.Teacher)))
            .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.StudentDepartment.Select(x => x.Department)))
            .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons))
            .ForMember(dest => dest.Parents, opt => opt.MapFrom(src => src.Parents));
        // CreateMap<StudentCreateDto, Student>();
        // CreateMap<StudentUpdateDto, Student>();
    }
}
