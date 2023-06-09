namespace Doggo.Infrastructure.Persistance;

using System.Reflection;
using Domain.Entities.DogOwner;
using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedules;
using Domain.Entities.User;
using Domain.Entities.Walker;
using Domain.Entities.Walker.Schedule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DoggoDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DoggoDbContext(DbContextOptions<DoggoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Walker> Walkers { get; set; }

    public DbSet<PossibleSchedule> PossibleSchedules { get; set; }

    public DbSet<DogOwner> DogOwners { get; set; }

    public DbSet<Dog> Dogs { get; set; }

    public DbSet<JobRequest> JobRequests { get; set; }

    public DbSet<RequiredSchedule> RequiredSchedules { get; set; }

    public DbSet<Job> Jobs { get; set; }
}