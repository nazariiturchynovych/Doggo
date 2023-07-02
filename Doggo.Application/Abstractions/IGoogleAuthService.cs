namespace Doggo.Application.Abstractions;

using Google.Apis.Auth;

public interface IGoogleAuthService
{
    public Task<GoogleJsonWebSignature.Payload> AuthenticateTokenAsync(string token);
}