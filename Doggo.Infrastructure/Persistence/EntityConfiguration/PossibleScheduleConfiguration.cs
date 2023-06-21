namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Walker.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PossibleScheduleConfiguration : IEntityTypeConfiguration<PossibleSchedule>
{
    public void Configure(EntityTypeBuilder<PossibleSchedule> builder)
    {
        builder.ToTable("PossibleSchedules");
    }
}