namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Application.Abstractions.Services;
using Domain.Constants;
using Domain.Results.External;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Options;

public class FacebookAuthService : IFacebookAuthService
{
    private readonly IOptions<FacebookAuthenticationOptions> _facebookSettings;
    private readonly HttpClient _httpClient;

    public FacebookAuthService(IOptions<FacebookAuthenticationOptions> facebookSettings, HttpClient httpClient)
    {
        _facebookSettings = facebookSettings;
        _httpClient = httpClient;
    }

    public async Task<FacebookTokenValidationResult> AuthenticateTokenAsync(string accessToken)
    {

        var formattedUrl = string.Format(
           FacebookConstants.TokenValidatorUrl,
            accessToken,
            _facebookSettings.Value.AppId,
            _facebookSettings.Value.AppSecret);

        var response = await _httpClient.GetAsync(formattedUrl);


        if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        {
            throw new Exception("Facebook call is unsuccessful");
        }

        var responseAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString)!;
    }

    public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
    {
        var formattedUrl = string.Format(
            FacebookConstants.UserInfoUrl,
            accessToken);

        var response = await _httpClient.GetAsync(formattedUrl);


        if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        {
            throw new Exception("Facebook call is unsuccessful");
        }

        var responseAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString)!;
    }
}