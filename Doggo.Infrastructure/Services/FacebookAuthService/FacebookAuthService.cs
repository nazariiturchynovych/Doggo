namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Domain.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class FacebookAuthService : IFacebookAuthService
{
    private readonly IOptions<FacebookAuthOptions> _facebookSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public FacebookAuthService(IOptions<FacebookAuthOptions> facebookSettings, IHttpClientFactory httpClientFactory)
    {
        _facebookSettings = facebookSettings;
        _httpClientFactory = httpClientFactory;
    }

    private const string TokenValidatorUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";

    private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";


    public async Task<FacebookTokenValidationResult> AuthenticateTokenAsync(string accessToken)
    {
        var formattedUrl = string.Format(
            TokenValidatorUrl,
            accessToken,
            _facebookSettings.Value.AppId,
            _facebookSettings.Value.AppSecret);

        var response = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);


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
            UserInfoUrl,
            accessToken);

        var response = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);


        if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        {
            throw new Exception("Facebook call is unsuccess");
        }

        var responseAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString)!;
    }
}