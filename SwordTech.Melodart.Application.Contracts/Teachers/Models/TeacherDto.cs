using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Application.Contract.Lessons.Models;

namespace SwordTech.Melodart.Application.Contract.Teachers.Models;

public class TeacherDto : BaseDto
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string FullName
    {
        get
        {
            return  $"{Name} {LastName}";
        }
    }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public List<DepartmentDto> Departments { get; set; }
    public List<LessonDto> Lessons { get; set; }
}
