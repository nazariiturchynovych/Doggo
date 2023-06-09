namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.JobRequest;
using Domain.Entities.JobRequest.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class JobRequestConfiguration : IEntityTypeConfiguration<JobRequest>
{
    public void Configure(EntityTypeBuilder<JobRequest> builder)
    {
        builder.ToTable("job_requests");

        builder.HasOne(x => x.RequiredSchedule)
            .WithOne(x => x.JobRequest)
            .HasForeignKey<RequiredSchedule>(x => x.JobRequestId);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.JobRequest)
            .HasForeignKey(x => x.JobRequestId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}