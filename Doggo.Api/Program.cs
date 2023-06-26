using Doggo.Application.Middlewares;
using Doggo.Domain.Constants;
using Doggo.Domain.Entities.User;
using Doggo.Extensions;
using Doggo.Hubs;
using Doggo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{level:u3}] {Message:lj}{NewLine}{Exception}",
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((hb, lc) => lc
        .WriteTo.Console().MinimumLevel.Information()
        .ReadFrom.Configuration(hb.Configuration));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGenWithJwt();

    builder.Services.AddDbContext<DoggoDbContext>(
        options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(ConnectionConstants.Postgres));
        });

    builder.Services
        .AddIdentity<User, Role>()
        .AddUserManager<UserManager<User>>()
        .AddEntityFrameworkStores<DoggoDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddCors(
        options =>
        {
            options.AddPolicy(
                name: "MyAllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:63342");
                    policy.WithHeaders("Access-Control-Allow-Origin");
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                });
        });

    builder.ConfigureIdentity();
    builder.RegisterAuthentication();
    builder.RegisterOptions();
    builder.RegisterServices();
    builder.RegisterRepositories();
    builder.RegisterBehaviours();
    builder.RegisterMiddlewares();
    builder.RegisterAwsServices();


    builder.Services.AddSignalR();


   // builder.Services.AddControllers().AddJsonOptions(x =>
   //  {
   //      x.JsonSerializerOptions.Converters.Add(new TimeOnlyFromStringConverter());
   //  });


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseCors("MyAllowSpecificOrigins");

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseSerilogRequestLogging();

    app.MapHub<NotificationsHub>("notifications-hub");

// app.SeedUsersAndRolesAsync().Wait();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Exception in program.cs occured");
}
finally
{
    Log.Information("shut down complete");
    Log.CloseAndFlush();
}