namespace Doggo.Infrastructure.Services.JWTTokenGeneratorService;

using Domain.Entities.User;

public interface IJwtTokenGeneratorService
{
    public string GenerateToken(User user);
}