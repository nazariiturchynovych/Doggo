namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.JobRequest.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PersonalIdentifierConfiguration : IEntityTypeConfiguration<PersonalIdentifier>
{
    public void Configure(EntityTypeBuilder<PersonalIdentifier> builder)
    {
        builder.ToTable("PersonalIdentifiers");
    }
}