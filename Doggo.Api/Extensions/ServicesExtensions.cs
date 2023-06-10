namespace Doggo.Extensions;

using System.Reflection;
using Domain.Options;
using Infrastructure.EmailService;
using Infrastructure.JWTTokenGeneratorService;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

public static class ServicesExtensions
{
    public static void ConfigureIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<IdentityOptions>(
            options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            });
    }

    public static void RegisterOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettingsOptions>(builder.Configuration.GetSection(nameof(JwtSettingsOptions)));
        builder.Services.Configure<SMTPOptions>(builder.Configuration.GetSection(nameof(SMTPOptions)));
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
        builder.Services.AddScoped<IEmailService, EmailService>();


        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
    }
}