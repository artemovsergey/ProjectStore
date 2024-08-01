using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectStoreContext>(opts =>
        {
            opts.UseNpgsql(configuration.GetConnectionString("Postgres") ??
                           "Host=localhost;Port=5432;Database=ProjectStore;Username=postgres;Password=root");
        });
        
        return services;
    }
}