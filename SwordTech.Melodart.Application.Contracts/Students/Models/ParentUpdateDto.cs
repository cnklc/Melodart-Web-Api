namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class ParentUpdateDto
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
    public string? Gender { get; set; }

    public string? Address { get; set; }
}
