namespace Doggo.Presentation;

using Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ActionResultFilter>();
        });

        builder.Services.AddSignalR();
        return builder.Services;


    }

}