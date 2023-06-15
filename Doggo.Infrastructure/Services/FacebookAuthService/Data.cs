#pragma warning disable CS8618
namespace Doggo.Infrastructure.Services.FacebookAuthService;

using Newtonsoft.Json;

public class Data
{
    [JsonProperty("app_id")]
    public string AppId { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("application")]
    public string Application { get; set; }

    [JsonProperty("data access expires at")]
    public long DataAccessExpiresAt { get; set; }

    [JsonProperty("expires_at")]
    public long ExpiresAt { get; set; }

    [JsonProperty("is valid")]
    public bool IsValid { get; set; }

    [JsonProperty("scopes")]
    public string[] Scopes { get; set; }

    [JsonProperty("user_id")]
    public string UserId { get; set; }
}