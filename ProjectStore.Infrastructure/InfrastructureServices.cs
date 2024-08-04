using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectStore.Domen.Models;
using ProjectStore.Infrastructure.Data;


namespace ProjectStore.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectStoreContext>(opts =>
        {
            opts.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
        });
        
        
        //Add ApplicationDbContext and SQL Server support
         services.AddDbContext<ApplicationContext>(options =>
             options.UseSqlServer(
                 configuration.GetConnectionString("DefaultConnection")
             )
         );
        
         // Замечание: проверка /Account/Login
         services.AddIdentity<ApplicationUser, IdentityRole>(options =>
             {
                 options.SignIn.RequireConfirmedAccount = true;
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequiredLength = 8;
             })
             .AddEntityFrameworkStores<ApplicationContext>();
        
         
         services.AddScoped<JwtHandler>();
        
        
        return services;
    }
}