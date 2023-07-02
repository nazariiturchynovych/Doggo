#pragma warning disable CS8618
namespace Doggo.Domain.Options;

public class GoogleAuthOptions
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
}