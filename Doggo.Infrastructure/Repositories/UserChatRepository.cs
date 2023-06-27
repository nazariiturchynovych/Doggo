namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.Chat;
using Persistence;

public class UserChatRepository : AbstractRepository<UserChat>, IUserChatRepository
{
    public UserChatRepository(DoggoDbContext context)
        : base(context)
    {
    }
}