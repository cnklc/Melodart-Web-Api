using System.ComponentModel.DataAnnotations;

namespace SwordTech.Melodart.Helper.Entity;

public abstract class Entity : IEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid CreatedUser { get; set; }
    public Guid UpdatedUser { get; set; }
    public bool IsDeleted { get; set; }
}
