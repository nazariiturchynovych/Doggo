namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.User.Documents;
using Domain.Enums;
using Persistence;

public class PersonalIdentifierRepository : AbstractRepository<PersonalIdentifier>, IPersonalIdentifierRepository
{
    public PersonalIdentifierRepository(DoggoDbContext context)
        : base(context)
    {
    }
}