using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.Domain._ManyToMany;
using SwordTech.Melodart.Domain.Classes;
using SwordTech.Melodart.Domain.Departments;
using SwordTech.Melodart.Domain.Finance;
using SwordTech.Melodart.Domain.Students;
using SwordTech.Melodart.Domain.Teachers;
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

    public DbSet<TeacherDepartment> TeacherDepartments { get; set; }
    public DbSet<StudentDepartment> StudentDepartments { get; set; }
    public DbSet<TeacherStudent> TeacherStudents { get; set; }

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
        optionsBuilder.UseSqlServer(@"server=swordtech.net,1433;Database=semboldi_melo;uid=semboldi_melo;pwd=d4Dq8Je6E;TrustServerCertificate=True;");
        // optionsBuilder.UseSqlServer(@"server=localhost,1433;Database=melodart;uid=sa;pwd=1q2w3E1q2w3E;TrustServerCertificate=True;");
        // optionsBuilder
        //     .UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Account>(b =>
        {
            b.HasMany(x => x.Transactions)
                .WithOne()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Student>(b =>
        {
            b.HasMany(x => x.Transactions)
                .WithOne()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasMany(x => x.StudentDepartment)
                .WithOne()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Teacher>(b =>
        {
            b.HasMany(x => x.Transactions)
                .WithOne()
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasMany(x => x.TeacherDepartments)
                .WithOne()
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Department>(b =>
        {
            b.HasMany(x => x.TeacherDepartments)
                .WithOne()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Transaction>(b =>
        {
            b.HasOne(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.Student)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.Teacher)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
