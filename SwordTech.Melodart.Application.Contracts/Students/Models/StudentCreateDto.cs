namespace SwordTech.Melodart.Application.Contract.Students.Models;

public class StudentCreateDto
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public DateTime Birthday { get; private set; }
    public string Address { get; private set; }
}
