using Doggo.Domain.Constants;
using Doggo.Domain.Entities.User;
using Doggo.Extensions;
using Doggo.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DoggoDbContext>( options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(ConnectionConstants.Postgres));
});

builder.Services
    .AddIdentity<User, Role>()
    .AddUserManager<UserManager<User>>()
    .AddEntityFrameworkStores<DoggoDbContext>()
    .AddDefaultTokenProviders();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();