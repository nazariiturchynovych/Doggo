namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.HasOne(x => x.JobRequest)
            .WithOne(x => x.Job)
            .HasForeignKey<JobRequest>(x => x.JobId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}