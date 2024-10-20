using SwordTech.Melodart.Application.Contract.Lessons.Models;
using SwordTech.Melodart.Domain.Contracts.Student;

namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class StudentCreateDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MothersName { get; set; }
    public string? MothersPhoneNumber { get; set; }
    public string? FathersName { get; set; }
    public string? FathersPhoneNumber { get; set; }
    public string? Description { get; set; }
    public GenderType Gender { get; set; }

    public string? Address { get; set; }

    // public List<LessonCreateDto> Lessons { get; set; }
}

   