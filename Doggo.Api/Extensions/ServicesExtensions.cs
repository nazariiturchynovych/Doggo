namespace Doggo.Extensions;

using Infrastructure.JWTTokenGeneratorService;
using Microsoft.AspNetCore.Identity;

public static class ServicesExtensions
{
    public static void ConfigureIdentity(this WebApplicationBuilder builder)
    {
       builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireDigit = false;
            options.Password.RequiredUniqueChars = 0;
        });
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
    }
}