using SwordTech.Melodart.Application.Contract.Base;

namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class StudentDto : BaseDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; }
}
