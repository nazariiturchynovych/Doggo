namespace Doggo.Infrastructure.JWTTokenGeneratorService;

using Domain.Entities.User;

public interface IJwtTokenGeneratorService
{
    public string GenerateToken(User user);
}