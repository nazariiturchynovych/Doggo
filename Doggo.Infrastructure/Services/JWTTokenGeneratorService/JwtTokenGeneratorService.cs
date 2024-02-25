namespace Doggo.Infrastructure.Services.JWTTokenGeneratorService;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions.Services;
using Domain.Entities.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Options;

public class JwtTokenGeneratorService : IJwtTokenGeneratorService
{
    private readonly JwtSettingsOptions _jwtSettings;

    public JwtTokenGeneratorService(IOptions<JwtSettingsOptions> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name!));
        }

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public ClaimsPrincipal? GetClaimsPrincipalFromToken(string token)
    {
        var configurationSettingsOptions
            = _jwtSettings;
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configurationSettingsOptions.Issuer,
            ValidAudience = configurationSettingsOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationSettingsOptions.Secret)),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }

    public bool IsJwtWithValidSecurityAlgorithm(SecurityToken token)
    {
        return (token is JwtSecurityToken jwtSecurityToken)
            && jwtSecurityToken.Header.Alg.Equals(
                   SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}