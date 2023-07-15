namespace Doggo.Infrastructure.Persistence;

using Domain.Entities.Chat;
using Domain.Entities.Dog;
using Domain.Entities.DogOwner;
using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedule;
using Domain.Entities.User;
using Domain.Entities.User.Documents;
using Domain.Entities.Walker;
using Domain.Entities.Walker.Schedule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DoggoDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DoggoDbContext(DbContextOptions<DoggoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //
        // foreach(var entity in builder.Model.GetEntityTypes())
        // {
        //     entity.PropertyNameToSnakeCase();
        // }
    }

    public DbSet<Walker> Walkers { get; set; }

    public DbSet<PossibleSchedule> PossibleSchedules { get; set; }

    public DbSet<DogOwner> DogOwners { get; set; }

    public DbSet<Dog> Dogs { get; set; }

    public DbSet<JobRequest> JobRequests { get; set; }

    public DbSet<RequiredSchedule> RequiredSchedules { get; set; }

    public DbSet<Job> Jobs { get; set; }

    public DbSet<PersonalIdentifier> PersonalIdentifiers { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<UserChat> UserChats { get; set; }
}

