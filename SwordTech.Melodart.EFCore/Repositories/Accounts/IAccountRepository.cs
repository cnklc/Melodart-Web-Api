using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.EFCore.EFCore;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.EFCore.Repositories.Accounts;

public interface IAccountRepository : IEfBaseRepository<Account>
{
    public Account GetAccount(Guid id);

    public void SaveChanges();
}


public class AccountRepository : EfBaseRepository<Account>, IAccountRepository
{
    public AccountRepository(ServiceDbContext context) : base(context)
    {
    }

    public Account GetAccount(Guid id)
    {
        return _dbSet.Include(x => x.Transactions.Where(t => !t.IsDeleted)).First(x => x.Id == id);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
