using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Classes;

public class Class : Entity
{
    public Class(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        Name = name;
        Description = description;
    }

    public Class()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
}
