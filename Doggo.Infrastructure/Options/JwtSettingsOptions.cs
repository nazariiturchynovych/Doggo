#pragma warning disable CS8618
namespace Doggo.Domain.Options;

public class JwtSettingsOptions
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiryMinutes { get; set; }
}