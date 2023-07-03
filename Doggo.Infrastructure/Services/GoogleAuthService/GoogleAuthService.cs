namespace Doggo.Infrastructure.Services.GoogleAuthService;

using Application.Abstractions.Services;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Options;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IOptions<GoogleAuthenticationOptions> _options;

    public GoogleAuthService(IOptions<GoogleAuthenticationOptions> options)
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