namespace SwordTech.Melodart.Application.Contract.Teachers.Models;

public class TeacherUpdateDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public List<Guid> Departments { get; set; }
}
