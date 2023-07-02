using Doggo.Api.Extensions;
using Doggo.Api.Hubs;
using Doggo.Application.Middlewares;
using Doggo.Domain.Constants;
using Doggo.Domain.Entities.User;
using Doggo.Infrastructure.Persistence;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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
                    policy.WithOrigins("http://localhost:63342")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
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

    app.MapHealthChecks("/_health", new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse 
    });

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseCors("MyAllowSpecificOrigins");

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseSerilogRequestLogging();

    app.MapHub<DoggoHub>("doggo-hub");

// app.SeedUsersAndRolesAsync().Wait();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex.Message, "Exception in program.cs occured");
}
finally
{
    Log.Information("shut down complete");
    Log.CloseAndFlush();
}