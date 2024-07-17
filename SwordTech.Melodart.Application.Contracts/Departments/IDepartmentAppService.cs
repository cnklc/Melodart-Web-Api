using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Domain.Departments;

namespace SwordTech.Melodart.Application.Contract.Departments;

public interface IDepartmentAppService : IAppService<Department, DepartmentDto, DepartmentDto, DepartmentCreatDto, DepartmentUpdateDto>
{
}
