namespace Doggo.Application.Helpers;

using Google.Apis.Auth;

public static class GoogleAuthHelper
{
    public static async Task<GoogleJsonWebSignature.Payload> AuthenticateTokenAsync(string token)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>() {"21451806702-4072m1adnb8gnmqda33tmqp69f1nlj1n.apps.googleusercontent.com"}
        };
        return await GoogleJsonWebSignature.ValidateAsync(token, settings);
    }
}