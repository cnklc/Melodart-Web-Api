namespace SwordTech.Melodart.Application.Contract.Users.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string ImageUrl { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public List<string> Authorizations { get; set; } = new List<string>();
}
