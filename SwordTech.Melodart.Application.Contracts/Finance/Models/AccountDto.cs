using SwordTech.Melodart.Application.Contract.Base;

namespace SwordTech.Melodart.Application.Contract.Finance.Models;

public class AccountDto : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }
 
}
