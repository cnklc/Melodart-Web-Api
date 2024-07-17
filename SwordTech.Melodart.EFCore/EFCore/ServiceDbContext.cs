using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.Domain.Classes;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.EFCore.EFCore;

public class ServiceDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Parent> Parents { get; set; }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Class> Classes { get; set; }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    entry.Entity.IsDeleted = false;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    entry.Entity.IsDeleted = true;
                    break;
            }
        }

        return base.SaveChanges();
    }

    // docker run --name sqlserver_container -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1q2w3E1q2w3E' -e 'MSSQL_PID=Developer' -p 1433:1433 -h sqlServer2022 -d mcr.microsoft.com/mssql/server:2022-latest

    // dotnet ef migrations add InitialCreate
    
    // dotnet ef database update  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server=localhost,1433;Database=melodart;uid=sa;pwd=1q2w3E1q2w3E;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }
}
