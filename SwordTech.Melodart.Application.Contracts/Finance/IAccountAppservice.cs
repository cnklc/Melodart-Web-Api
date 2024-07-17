using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;

namespace SwordTech.Melodart.Application.Contract.Finance;

public interface IAccountAppservice : IAppService<Account, AccountDto, AccountDto, AccountCreateDto, AccountUpdateDto>
{
}
