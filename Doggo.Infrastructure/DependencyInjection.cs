namespace Doggo.Infrastructure;

using Amazon.Runtime;
using Amazon.S3;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.UnitOfWork;
using Application.Abstractions.Services;
using Application.Abstractions.SqlConnectionFactory;
using Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;
using Persistence;
using Persistence.SqlConnectionFactory;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Repositories;
using Repositories.UnitOfWork;
using Services.CacheService;
using Services.CurrentUserService;
using Services.EmailService;
using Services.FacebookAuthService;
using Services.GoogleAuthService;
using Services.ImageService;
using Services.JWTTokenGeneratorService;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .RegisterRepositories()
            .RegisterServices(builder.Configuration)
            .RegisterOptions(builder.Configuration)
            .RegisterAwsServices(builder.Configuration)
            .RegisterDbContext(builder.Configuration)
            .RegisterHttpClients();

        return builder.Services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IDogRepository, DogRepository>()
            .AddScoped<IJobRepository, JobRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IWalkerRepository, WalkerRepository>()
            .AddScoped<IDogOwnerRepository, DogOwnerRepository>()
            .AddScoped<IJobRequestRepository, JobRequestRepository>()
            .AddScoped<IPossibleScheduleRepository, PossibleScheduleRepository>()
            .AddScoped<IRequiredScheduleRepository, RequiredScheduleRepository>()
            .AddScoped<IPersonalIdentifierRepository, PersonalIdentifierRepository>()
            .AddScoped<IChatRepository, ChatRepository>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IUserChatRepository, UserChatRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddScoped<IGoogleAuthService, GoogleAuthService>()
            .AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<ICurrentUserService, CurrenUserService>()
            .AddSingleton<ICacheService, CacheService>()
            .AddScoped<IFacebookAuthService, FacebookAuthService>()
            .AddSingleton<IImageService, ImageService>()
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        services.AddHttpContextAccessor()
            .AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = configuration.GetConnectionString(ConnectionConstants.Redis);
                });

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .Configure<S3Options>(configuration.GetSection("AWS:S3"))
            .Configure<JwtSettingsOptions>(configuration.GetSection(nameof(JwtSettingsOptions)))
            .Configure<SMTPOptions>(configuration.GetSection(nameof(SMTPOptions)))
            .Configure<FacebookAuthenticationOptions>(configuration.GetSection(nameof(FacebookAuthenticationOptions)))
            .Configure<GoogleAuthenticationOptions>(configuration.GetSection(nameof(GoogleAuthenticationOptions)));

        return services;
    }

    private static void RegisterHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IFacebookAuthService, FacebookAuthService>(
                options =>
                {
                    options.BaseAddress = new Uri(FacebookConstants.BaseUrl);
                })
            .AddTransientHttpErrorPolicy(
                x =>
                    x.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3)));
    }

    private static IServiceCollection RegisterAwsServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var awsOptions = configuration.GetAWSOptions();
        var credentials = new BasicAWSCredentials(
            configuration.GetSection("AWS:IAM:AccessKey").Value,
            configuration.GetSection("AWS:IAM:SecretAccessKey").Value);
        awsOptions.Credentials = credentials;

        services.AddDefaultAWSOptions(awsOptions);

        services.AddAWSService<IAmazonS3>();

        return services;
    }

    private static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<DoggoDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(ConnectionConstants.Postgres))
                    .UseSnakeCaseNamingConvention();
            });
        return services;
    }
}