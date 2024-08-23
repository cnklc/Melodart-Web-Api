using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;

namespace SwordTech.Melodart.Application.Contract.Finance;

public interface ITransactionAppService : IAppService<Transaction, TransactionDto, TransactionDto>
{
    Task Delete(Guid id);
    Task<TransactionDto> Update(Guid id, TransactionUpdateDto input);
}
