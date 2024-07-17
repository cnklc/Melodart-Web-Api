using System.Collections.ObjectModel;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Students;

public class Student : Entity
{
    public Student(string name, string lastName, string phoneNumber, string email, DateTime birthday, string address, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        if (string.IsNullOrEmpty(lastName))
        {
            throw new DomainException("Soyisim alanı boş olamaz.");
        }

        Name = name;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Birthday = birthday;
        Address = address;
        Description = description;
    }

    public Student()
    {
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public DateTime Birthday { get; private set; }
    public string Address { get; private set; }

    public Collection<Parent> Parents { get; private set; }
    public Collection<Transaction> Transactions { get; private set; }

    public void AddParent(Parent parent)
    {
        Parents.Add(parent);
    }
}
