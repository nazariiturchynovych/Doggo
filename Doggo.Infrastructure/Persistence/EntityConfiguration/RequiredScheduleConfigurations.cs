namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.JobRequest.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RequiredScheduleConfigurations: IEntityTypeConfiguration<RequiredSchedule>
{
    public void Configure(EntityTypeBuilder<RequiredSchedule> builder)
    {
        builder.ToTable("required_schedules");
    }
}