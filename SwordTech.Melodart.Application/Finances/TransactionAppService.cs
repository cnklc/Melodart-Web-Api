using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Finance;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Finances;

public class TransactionAppService : AppService<Transaction, TransactionDto, TransactionDto>, ITransactionAppService
{
    public TransactionAppService(IEfBaseRepository<Transaction> repository, IMapper mapper) : base(repository, mapper)
    {
    }
    
    public async Task<TransactionDto> Update(Guid id, TransactionUpdateDto input)
    {
        var entity = _repository.GetById(id);
        _mapper.Map(input,entity);
        _repository.Update(entity);

        return await GetById(entity.Id);
    }

    // public async Task<TransactionDto> Update(Guid id, TransactionUpdateDto input)
    // {
    //     var entity = _repository.GetById(id);
    //
    //     entity.SetDescription(input.Description);
    //     entity.SetAmount(input.Amount);
    //
    //     _repository.Update(entity);
    //
    //     return await GetById(entity.Id);
    // }

    public async Task Delete(Guid id)
    {
        var entity = _repository.GetById(id);

        if (entity != null)
        {
            _repository.Delete(entity);
        }
    }
   
}
