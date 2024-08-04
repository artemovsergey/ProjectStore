using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectStore.Infrastructure.Data;

/// <summary>
/// Клаас для работы автогенерации restAPI черезе IDE по модели и контексту
/// если конструктор с параметрами
/// </summary>
public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ProjectStore;User Id=sa;Password=;TrustServerCertificate=True");

        return new ApplicationContext(optionsBuilder.Options);
    }
}