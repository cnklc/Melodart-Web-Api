using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;

namespace SwordTech.Melodart.Application.Contract.Finance;

public interface IAccountAppService : IAppService<Account, AccountDto, AccountDetailDto, AccountCreateDto, AccountUpdateDto>
{
    Task<TransactionDto> AddTransaction(TransactionCreateDto input);
}
