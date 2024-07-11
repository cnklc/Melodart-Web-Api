using Microsoft.AspNetCore.Http;

namespace SwordTech.Melodart.Application.Contract.Users.Models;

public class UserCreateDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Title { get; set; }
    public IFormFile Image { get; set; }

    public List<string> Authorizations { get; set; }
}
