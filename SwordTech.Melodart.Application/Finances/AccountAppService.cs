using AutoMapper;
using SwordTech.Melodart.Application.Base;
using SwordTech.Melodart.Application.Contract.Finance;
using SwordTech.Melodart.Application.Contract.Finance.Models;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
using SwordTech.Melodart.EFCore.Repositories.Accounts;
using SwordTech.Melodart.EFCore.Repositories.Base;
using SwordTech.Melodart.Helper.Error;

namespace SwordTech.Melodart.Application.Finances;

public class AccountAppService : AppService<Account, AccountDto, AccountDetailDto, AccountCreateDto, AccountUpdateDto>, IAccountAppService
{

    private readonly ITransactionAppService _transactionAppService;
    private readonly IEfBaseRepository<Student> _studentRepository;
    private readonly IEfBaseRepository<Teacher> _teacherRepository;

    public AccountAppService(IAccountRepository repository, IMapper mapper, ITransactionAppService transactionAppService, IEfBaseRepository<Student> studentRepository, IEfBaseRepository<Teacher> teacherRepository) : base(repository, mapper)
    {

        _transactionAppService = transactionAppService;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<TransactionDto> AddTransaction(TransactionCreateDto input)
    {
        var account = _repository.GetById(input.AccountId);

        var transaction = new Transaction(input.Description, input.Amount);
        if (input.StudentId.HasValue)
        {
            var student = _studentRepository.GetById(input.StudentId.Value);
            transaction.AddStudent(student);
        }

        if (input.TeacherId.HasValue)
        {
            var teacher = _teacherRepository.GetById(input.TeacherId.Value);
            transaction.AddTeacher(teacher);
        }

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
