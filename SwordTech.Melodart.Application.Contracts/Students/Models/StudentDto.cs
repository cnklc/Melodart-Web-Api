using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Departments.Models;
using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Application.Contract.Teachers.Models;
using SwordTech.Melodart.Domain.Contracts.Student;

namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class StudentDto : BaseDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string FullName
    {
        get
        {
            return $"{Name} {LastName}";
        }
    }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; }
    public string MothersName
    {
        get
        {
            var mother = Parents?.FirstOrDefault(x => x.ParentType == ParentType.Mother);

            if (mother != null)
            {
                return mother.Name + " " + mother.LastName;
            }
            return "";
        }
    }
    public string MothersPhoneNumber
    {
        get
        {
            var mother = Parents?.FirstOrDefault(x => x.ParentType == ParentType.Mother);

            if (mother != null)
            {
                return mother.PhoneNumber;
            }
            return "";
        }
    }
    public string FathersName
    {
        get
        {
            var father = Parents?.FirstOrDefault(x => x.ParentType == ParentType.Father);

            if (father != null)
            {
                return father.Name + " " + father.LastName;
            }
            return "";
        }
    }
    public string FathersPhoneNumber {   get
    {
        var father = Parents?.FirstOrDefault(x => x.ParentType == ParentType.Father);

        if (father != null)
        {
            return father.PhoneNumber;
        }
        return "";
    } }
    public GenderType Gender { get; set; }

    public List<DepartmentDto> Departments { get; set; }
    public List<TeacherDto> Teachers { get; set; }
    public List<LessonDto> Lessons { get; set; }
    public List<ParentDto> Parents { get; set; }

}
