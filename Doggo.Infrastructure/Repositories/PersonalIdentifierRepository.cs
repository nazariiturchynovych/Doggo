namespace Doggo.Infrastructure.Repositories;

using Base;
using Doggo.Application.Abstractions.Persistence.Read;
using Domain.Entities.User.Documents;
using Persistence;

public class PersonalIdentifierRepository : AbstractRepository<PersonalIdentifier>, IPersonalIdentifierRepository
{
    public PersonalIdentifierRepository(DoggoDbContext context)
        : base(context)
    {
    }
}