using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Departments;

public class Department : Entity
{
    public Department(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        Name = name;
        Description = description;
    }

    public Department()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
}
