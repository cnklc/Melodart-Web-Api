using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SwordTech.Melodart.Application.Classes;
using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Classes;
using SwordTech.Melodart.Application.Contract.Departments;
using SwordTech.Melodart.Application.Contract.Finance;
using SwordTech.Melodart.Application.Contract.Students;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Application.Contract.Users.Validators;
using SwordTech.Melodart.Application.Departments;
using SwordTech.Melodart.Application.Finances;
using SwordTech.Melodart.Application.Students;
using SwordTech.Melodart.Application.Users;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.EFCore.EFCore;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.EFCore.Repositories.Accounts;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application;

public class AppStartup
{
    public static void ConfigureService(IServiceCollection services)
    {
        // services.AddIdentityCore<AppUser>();
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<ServiceDbContext>()
            .AddDefaultTokenProviders();

        services.AddMemoryCache();

        services.AddAutoMapper(typeof(AutoMapperProfile));

        AddDI(services);

        Configure();
    }

    public static void Configure()
    {
        // Init.AllData();
    }

    static void AddDI(IServiceCollection services)
    {
        services.AddDbContext<ServiceDbContext>();

        // repositories
        services.AddTransient(typeof(IEfBaseRepository<>), typeof(EfBaseRepository<>));
        services.AddTransient<IAccountRepository, AccountRepository>();

        // services
        services.AddTransient<IUserAppService, UserAppService>();
        services.AddTransient<IAuthAppService, AuthAppAppService>();
        services.AddTransient<IDepartmentAppService, DepartmentAppService>();
        services.AddTransient<IClassAppService, ClassAppService>();
        services.AddTransient<ITransactionAppService, TransactionAppService>();
        services.AddTransient<IAccountAppService, AccountAppService>();
        services.AddTransient<IStudentAppService, StudentAppService>();
        services.AddTransient<IParentAppService, ParentAppService>();

        // Helper
        // services.AddTransient<IMailService, MailService>(); // smtp bilgilerini kontrol et

        //Validators
        services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
    }
}
