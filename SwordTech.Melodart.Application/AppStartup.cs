using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SwordTech.Melodart.Application.Mapper;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.EFCore.EFCore;

namespace SwordTech.Melodart.Application;

public class AppStartup
{
    public static void ConfigureService(IServiceCollection services)
    {
        // services.AddIdentityCore<AppUser>();
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<ServiceDbContext>()
            .AddDefaultTokenProviders();

        services.AddAutoMapper(typeof(AutoMapperProfile));

        Configure();
    }

    public static void Configure()
    {
        // Init.AllData();
    }
}
