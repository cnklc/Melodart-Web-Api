namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class StudentUpdateDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Address { get; set; }
    
    public List<Guid> Departments { get; set; }
    public List<Guid> Teachers { get; set; }
}
