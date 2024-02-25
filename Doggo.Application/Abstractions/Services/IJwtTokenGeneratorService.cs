namespace Doggo.Application.Abstractions.Services;

using System.Security.Claims;
using Domain.Entities.User;
using Microsoft.IdentityModel.Tokens;

public interface IJwtTokenGeneratorService
{
    public string GenerateToken(User user);

    public ClaimsPrincipal? GetClaimsPrincipalFromToken(string token);

    public bool IsJwtWithValidSecurityAlgorithm(SecurityToken token);
}