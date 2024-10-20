using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Domain.Contracts.Student;

namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class ParentDto : BaseDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public ParentType ParentType { get; set; }
}
