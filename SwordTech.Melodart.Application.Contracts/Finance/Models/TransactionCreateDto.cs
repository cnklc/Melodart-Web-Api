namespace SwordTech.Melodart.Application.Contract.Finance.Models;

public class TransactionCreateDto
{
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public Guid AccountId { get; set; }
    public Guid? StudentId { get; set; }
    public Guid? TeacherId { get; set; }
}
