namespace Doggo.Application;

using System.Reflection;
using Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this WebApplicationBuilder builder)
    {
       builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
       builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

       builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
       builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SavingChangesPipeLineBehaviour<,>));

        return builder.Services;
    }
}