using SwordTech.Melodart.Helper.Entity;
using Microsoft.AspNetCore.Identity;

namespace SwordTech.Melodart.Domain.User;


public class AppUser : IdentityUser<Guid>, IEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; }
    public string Title { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid CreatedUser { get; set; }
    public Guid UpdatedUser { get; set; }
    public bool IsDeleted { get; set; }
}
