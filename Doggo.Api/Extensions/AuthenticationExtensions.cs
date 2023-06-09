namespace Doggo.Extensions;

using System.Text;
using Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationExtensions
{
    public static void RegisterAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(
                options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(
                options =>
                {
                    var configurationSettingsOptions
                        = builder.Services.OfType<IOptions<JwtSettingsOptions>>().FirstOrDefault().Value;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configurationSettingsOptions.Issuer,
                        ValidAudience = configurationSettingsOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationSettingsOptions.Secret)),
                    };
                });

    }
}