namespace Doggo.Infrastructure.Repositories;

using Application.Abstractions.Repositories;
using Base;
using Domain.Entities.User.Documents;
using Persistence;

public class PersonalIdentifierRepository : AbstractRepository<PersonalIdentifier>, IPersonalIdentifierRepository
{
    public PersonalIdentifierRepository(DoggoDbContext context)
        : base(context)
    {
    }
}