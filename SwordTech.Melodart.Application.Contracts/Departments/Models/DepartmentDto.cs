using SwordTech.Melodart.Application.Contract.Base;

namespace SwordTech.Melodart.Application.Contract.Departments.Models;

public class DepartmentDto : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
