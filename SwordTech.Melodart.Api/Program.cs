using FluentValidation;
using FluentValidation.AspNetCore;
using SwordTech.Melodart.Api.Middlewares;
using SwordTech.Melodart.Application;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Validations;
using SwordTech.Melodart.Application.Users;
using SwordTech.Melodart.EFCore.EFCore;
using SwordTech.Melodart.EFCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

Configure(builder.Build()).Run();

void ConfigureServices(IServiceCollection services)
{
    AddDI(services);

    AppStartup.ConfigureService(services);

    // Add services to the container.
    services.AddControllers();

    // FluentValidation doğrulayıcılarını kaydedin
    services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
    services.AddFluentValidationAutoValidation();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddHealthChecks();
}

WebApplication Configure(WebApplication app)
{
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapHealthChecks("/healthz");

    // custom middlewares
    app.UseLogginMiddleware();
    app.UseErrorMiddleware();
    // custom middlewares

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    return app;
}

void AddDI(IServiceCollection services)
{
}
