using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Finance;

public class Account : Entity
{
    public Account(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("İsim alanı boş olamaz.");
        }

        Name = name;
        Description = description;
    }

    public Account()
    {
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public List<Transaction> Transactions { get; set; }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }

    public void AddStudentTransaction(Student student, Transaction transaction)
    {
        if (transaction.Amount <= 0)
        {
            throw new DomainException("öğrenci ödeme işleminde tutar 0'dan küçük olamaz.");
        }

        student.Transactions.Add(transaction);
    }

    public void AddTeacherTransaction(Teacher teacher, Transaction transaction)
    {
        if (transaction.Amount >= 0)
        {
            throw new DomainException("Öğretmen ödeme işleminde tutar 0'dan büyük olamaz.");
        }

        teacher.Transactions.Add(transaction);
    }
}
