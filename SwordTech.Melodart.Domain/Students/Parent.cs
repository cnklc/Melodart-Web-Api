using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Students;

public class Parent : Entity
{
    public Parent(string name, string lastName, string phoneNumber, string email, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        if (string.IsNullOrEmpty(lastName))
        {
            throw new DomainException("Soyisim alanı boş olamaz.");
        }

        if (string.IsNullOrEmpty(phoneNumber))
        {
            throw new DomainException("Soyisim alanı boş olamaz.");
        }

        Name = name;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Description = description;
    }

    public Parent()
    {
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }

    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }
}
