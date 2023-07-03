namespace Doggo.Api.Extensions;

using Domain.Constants;
using Domain.Entities.User;
using HealthChecks.Aws.S3;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Middlewares;

public static class ServicesExtensions
{
    public static void RegisterAndConfigureIdentity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentity<User, Role>()
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<DoggoDbContext>()
            .AddDefaultTokenProviders();


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

    public static void RegisterCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options =>
            {
                options.AddPolicy(
                    name: "MyAllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:63342")
                            .AllowAnyHeader()
                            .WithMethods("GET", "POST")
                            .AllowCredentials();
                    });
            });
    }

    public static void RegisterHealthCheckServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(ConnectionConstants.Postgres)!)
            .AddS3(x => x = builder.Configuration.GetSection("AWS:S3").Get<S3BucketOptions>()!)
            .AddRedis(builder.Configuration.GetConnectionString(ConnectionConstants.Redis)!)
            .AddSignalRHub("https://localhost:7278/doggo-hub");
    }


    public static void RegisterMiddlewares(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
    }

    public static IServiceCollection AddSwaggerGenWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "JWTToken_Auth_API",
                        Version = "v1"
                    });
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description
                            = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer token\"",
                    });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
            });
        return services;
    }

}