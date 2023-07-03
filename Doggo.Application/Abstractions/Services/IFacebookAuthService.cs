namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Domain.Results.External;

public interface IFacebookAuthService
{
    public Task<FacebookTokenValidationResult> AuthenticateTokenAsync(string accessToken);

    public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
}