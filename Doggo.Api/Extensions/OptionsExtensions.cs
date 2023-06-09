namespace Doggo.Extensions;

using Domain.Options;

public static class OptionsExtensions
{
    public static void RegisterOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettingsOptions>(builder.Configuration.GetSection(nameof(JwtSettingsOptions)));
    }
}