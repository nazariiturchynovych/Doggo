using Doggo.Domain.Constants;
using Doggo.Domain.Entities.User;
using Doggo.Extensions;
using Doggo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJwt();

builder.Services.AddDbContext<DoggoDbContext>( options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(ConnectionConstants.Postgres));
});

builder.Services
    .AddIdentity<User, Role>()
    .AddUserManager<UserManager<User>>()
    .AddEntityFrameworkStores<DoggoDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy  =>
        {
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// app.SeedUsersAndRolesAsync().Wait();

app.Run();
