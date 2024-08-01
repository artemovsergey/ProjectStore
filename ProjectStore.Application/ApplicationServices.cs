using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectStore.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddLogging();

        // Add MediatR services and register services from the current assembly
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly()));

        return services;
    }
}