using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Application.Contract.Users.Validations;
using SwordTech.Melodart.Application.Mapper;
using SwordTech.Melodart.Application.Users;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.EFCore.EFCore;
using SwordTech.Melodart.EFCore.Repositories;

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

        // services
        services.AddTransient<IUserAppService, UserAppService>();
        services.AddTransient<IAuthAppService, AuthAppAppService>();

        // Helper
        // services.AddTransient<IMailService, MailService>(); // smtp bilgilerini kontrol et

        //Validators
        services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
    }
}
