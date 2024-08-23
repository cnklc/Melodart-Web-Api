using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Students.Models;
using SwordTech.Melodart.Application.Contract.Teachers.Models;

namespace SwordTech.Melodart.Application.Contract.Finance.Models;

public class TransactionDto : BaseDto
{
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public Guid AccountId { get; set; }
    public AccountDto Account { get; set; }

    public Guid? StudentId { get; set; }
    public StudentDto Student { get; set; }

    public Guid? TeacherId { get; set; }
    public TeacherDto Teacher { get; set; }
}
