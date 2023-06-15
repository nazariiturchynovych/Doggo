#pragma warning disable CS8618
namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Newtonsoft.Json;

public class FacebookUserInfoResult
{
    [JsonProperty("first name")]
    public string FirstName { get; set; }

    [JsonProperty("last name")]
    public string LastName { get; set; }

    [JsonProperty("picture")]
    public FacebookPicture Picture { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    public class FacebookPicture
    {
        [JsonProperty("data")]
        public FacebookPictureData Data { get; set; }
    }
}

public class FacebookPictureData
{
    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("is silhouette")]
    public bool IsSilhouette { get; set; }

    [JsonProperty("ur]")]
    public Uri Url { get; set; }

    [JsonProperty("width")]
    public long Width { get; set; }
}

public class FacebookTokenValidationResult
{
    [JsonProperty("data")]
    public Data Data { get; set; }
}