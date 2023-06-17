namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Domain.Constants;
using Domain.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class FacebookAuthService : IFacebookAuthService
{
    private readonly IOptions<FacebookAuthOptions> _facebookSettings;
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;

    public FacebookAuthService(IOptions<FacebookAuthOptions> facebookSettings, HttpClient httpClient, IHttpClientFactory httpClientFactory)
    {
        _facebookSettings = facebookSettings;
        _httpClient = httpClient;
        _httpClientFactory = httpClientFactory;
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
            throw new Exception("Facebook call is unsuccess");
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
            throw new Exception("Facebook call is unsuccess");
        }

        var responseAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString)!;
    }
}