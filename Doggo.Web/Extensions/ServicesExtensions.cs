namespace Doggo.Api.Extensions;

using System.Reflection;
using Amazon.Runtime;
using Amazon.S3;
using Doggo.Application.Behaviours;
using Doggo.Application.Middlewares;
using Domain.Constants;
using Domain.Options;
using FluentValidation;
using HealthChecks.Aws.S3;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Infrastructure.Services.CurrentUserService;
using Infrastructure.Services.EmailService;
using Infrastructure.Services.FacebookAuthService;
using Infrastructure.Services.ImageService;
using Infrastructure.Services.JWTTokenGeneratorService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Contrib.WaitAndRetry;

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
        builder.Services.Configure<S3Options>(builder.Configuration.GetSection("AWS:S3"));
        builder.Services.Configure<JwtSettingsOptions>(builder.Configuration.GetSection(nameof(JwtSettingsOptions)));
        builder.Services.Configure<SMTPOptions>(builder.Configuration.GetSection(nameof(SMTPOptions)));
        builder.Services.Configure<FacebookAuthOptions>(builder.Configuration.GetSection(nameof(FacebookAuthOptions)));
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(ConnectionConstants.Postgres)!)
            .AddS3(x => x = builder.Configuration.GetSection("AWS:S3").Get<S3BucketOptions>()!)
            .AddRedis(builder.Configuration.GetConnectionString(ConnectionConstants.Redis)!)
            .AddSignalRHub("https://localhost:7278/doggo-hub");

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        builder.Services.AddScoped<IUrlHelper>(
            x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!);
            });

        builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<ICurrentUserService, CurrenUserService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddScoped<IFacebookAuthService, FacebookAuthService>();
        builder.Services.AddSingleton<IImageService, ImageService>();

        builder.Services.AddHttpClient<IFacebookAuthService, FacebookAuthService>(
                options =>
                {
                    options.BaseAddress = new Uri(FacebookConstants.BaseUrl);
                })
            .AddTransientHttpErrorPolicy(
                x => x.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3)));

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
        builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString(ConnectionConstants.Redis);
            });
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IDogRepository, DogRepository>();
        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IWalkerRepository, WalkerRepository>();
        builder.Services.AddScoped<IDogOwnerRepository, DogOwnerRepository>();
        builder.Services.AddScoped<IJobRequestRepository, JobRequestRepository>();
        builder.Services.AddScoped<IPossibleScheduleRepository, PossibleScheduleRepository>();
        builder.Services.AddScoped<IRequiredScheduleRepository, RequiredScheduleRepository>();
        builder.Services.AddScoped<IPersonalIdentifierRepository, PersonalIdentifierRepository>();
        builder.Services.AddScoped<IChatRepository, ChatRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<IUserChatRepository, UserChatRepository>();
    }

    public static void RegisterBehaviours(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
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

    public static void RegisterAwsServices(this WebApplicationBuilder builder)
    {
        var awsOptions = builder.Configuration.GetAWSOptions();
        var credentials = new BasicAWSCredentials(
            builder.Configuration.GetSection("AWS:IAM:AccessKey").Value,
            builder.Configuration.GetSection("AWS:IAM:SecretAccessKey").Value);
        awsOptions.Credentials = credentials;

        builder.Services.AddDefaultAWSOptions(awsOptions);

        builder.Services.AddAWSService<IAmazonS3>();
    }
}