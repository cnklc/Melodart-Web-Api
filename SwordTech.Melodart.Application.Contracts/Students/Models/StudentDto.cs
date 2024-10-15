using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Application.Contract.Teachers.Models;

namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class StudentDto : BaseDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string FullName
    {
        get
        {
          return  $"{Name} {LastName}";
        }
    }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; }
    
    public List<DepartmentDto> Departments { get; set; }
    public List<TeacherDto> Teachers { get; set; }
    public List<LessonDto> Lessons { get; set; }

}
