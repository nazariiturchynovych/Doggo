// ReSharper disable InconsistentNaming

#pragma warning disable CS8618
namespace Doggo.Domain.Options;
public class SMTPOptions
{
    public string Host { get; init; }

    public int Port { get; init; }

    public string UserName { get; init; }

    public string Password { get; init; }
}