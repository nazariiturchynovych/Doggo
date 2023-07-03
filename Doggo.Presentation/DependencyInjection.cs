namespace Doggo.Presentation;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();
        return builder.Services;
    }

}