namespace Doggo.Infrastructure.Services.GoogleAuthService;

using Doggo.Application.Abstractions;
using Doggo.Domain.Options;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IOptions<GoogleAuthOptions> _options;

    public GoogleAuthService(IOptions<GoogleAuthOptions> options)
    {
        _options = options;
    }

    public async Task<GoogleJsonWebSignature.Payload> AuthenticateTokenAsync(string token)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>() {_options.Value.ClientId}
        };
        return await GoogleJsonWebSignature.ValidateAsync(token, settings);
    }
}