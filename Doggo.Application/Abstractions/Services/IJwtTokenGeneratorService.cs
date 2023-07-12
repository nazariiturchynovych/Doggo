namespace Doggo.Application.Abstractions.Services;

using Domain.Entities.User;

public interface IJwtTokenGeneratorService
{
    public string GenerateToken(User user);
}