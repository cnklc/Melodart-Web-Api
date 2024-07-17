using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Departments;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.EFCore.Repositories;

namespace SwordTech.Melodart.Application.Departments;

public class DepartmentAppService : AppService<Department, DepartmentDto, DepartmentDto, DepartmentCreatDto, DepartmentUpdateDto>, IDepartmentAppService
{
    public DepartmentAppService(IEfBaseRepository<Department> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
