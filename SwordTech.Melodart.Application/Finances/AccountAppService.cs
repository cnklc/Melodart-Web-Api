using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Finance;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.EFCore.Repositories.Accounts;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Application.Finances;

public class AccountAppService : AppService<Account, AccountDto, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountAppService
{
    private readonly IAccountRepository _repository;
    private readonly ITransactionAppService _transactionAppService;

    public AccountAppService(IAccountRepository repository, IMapper mapper, ITransactionAppService transactionAppService) : base(repository, mapper)
    {
        _repository = repository;
        _transactionAppService = transactionAppService;
    }

    public async Task<TransactionDto> AddTransaction(TransactionCreateDto input)
    {
        var account = _repository.GetById(input.AccountId);

        var transaction = new Transaction(input.Description, input.Amount);
        account.AddTransaction(transaction);

        _repository.Update(account);

        return await _transactionAppService.GetById(transaction.Id);
    }

    public override async Task Delete(Guid id)
    {
        var entity = _repository.GetById(id);

        if (entity.Transactions.Any())
        {
            throw new BusinessException("Kasa içinde hareket olduğu için silinemez.");
        }

        if (entity != null)
        {
            _repository.Delete(entity);
        }
    }
}
