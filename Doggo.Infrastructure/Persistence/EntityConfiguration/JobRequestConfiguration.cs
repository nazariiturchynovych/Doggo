namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class JobRequestConfiguration : IEntityTypeConfiguration<JobRequest>
{
    public void Configure(EntityTypeBuilder<JobRequest> builder)
    {
        builder.ToTable("JobRequests");

        builder.HasOne(x => x.RequiredSchedule)
            .WithOne(x => x.JobRequest)
            .HasForeignKey<RequiredSchedule>(x => x.JobRequestId);

        builder.HasOne(x => x.Job)
            .WithOne(x => x.JobRequest)
            .HasForeignKey<Job>(x => x.JobRequestId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}