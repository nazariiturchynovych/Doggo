namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Domain.Entities.External;

public interface IFacebookAuthService
{
    public Task<FacebookTokenValidationResult> AuthenticateTokenAsync(string accessToken);

    public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
}