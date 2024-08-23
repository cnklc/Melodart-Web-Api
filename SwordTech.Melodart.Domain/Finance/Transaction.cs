using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.Helper.Entity;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Domain.Finance;

public class Transaction : Entity
{
    public Transaction(string description, decimal amount)
    {
        if (string.IsNullOrEmpty(description))
        {
            throw new DomainException("Açıklama alanı boş olamaz.");
        }

        // Id = Guid.NewGuid();

        Description = description;
        Amount = amount;
    }

    public Transaction()
    {
    }

    public string Description { get; private set; }
    public decimal Amount { get; private set; }

    public Guid AccountId { get; private set; }
    public virtual Account Account { get; private set; }

    public Guid? StudentId { get; private set; }
    public virtual Student Student { get; private set; }

    public Guid? TeacherId { get; private set; }
    public virtual Teacher Teacher { get; private set; }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetAmount(decimal amount)
    {
        Amount = amount;
    }

    public void SetAccount(Guid id)
    {
        AccountId = id;
    }
}
