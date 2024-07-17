using System.Collections.ObjectModel;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.Domain.Teachers;

public class Teacher : Entity
{
    public Teacher(string name, string lastName, string phoneNumber, string email, string description)
    {
        Name = name;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Description = description;
    }

    public Teacher()
    {
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }

    public Collection<Transaction> Transactions { get; private set; }
    public Collection<Student> Students { get; private set; }
}
