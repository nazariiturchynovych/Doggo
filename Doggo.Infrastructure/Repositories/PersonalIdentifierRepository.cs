namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.User.Documents;
using Persistence;

public class PersonalIdentifierRepository : AbstractRepository<PersonalIdentifier>, IPersonalIdentifierRepository
{
    public PersonalIdentifierRepository(DoggoDbContext context)
        : base(context)
    {
    }
}