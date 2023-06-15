namespace Doggo.Infrastructure.Services.FacebookAuthService;

public interface IFacebookAuthService
{
    public Task<FacebookTokenValidationResult> AuthenticateTokenAsync(string accessToken);

    public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
}