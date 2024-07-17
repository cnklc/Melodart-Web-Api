using AutoMapper;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Domain.Departments;

namespace SwordTech.Melodart.Application.Contract.Departments.Mappers;

public class DepartmentMapperProfile : Profile
{
    public DepartmentMapperProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentCreatDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>();
    }
}
