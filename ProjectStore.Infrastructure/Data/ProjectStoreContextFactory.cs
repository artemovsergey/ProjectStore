using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectStore.Infrastructure.Data;

/// <summary>
/// Клаас для работы автогенерации restAPI черезе IDE по модели и контексту
/// если конструктор с параметрами
/// </summary>
public class ProjectStoreContextFactory : IDesignTimeDbContextFactory<ProjectStoreContext>
{
    public ProjectStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjectStoreContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProjectStore;Username=postgres;Password=root");

        return new ProjectStoreContext(optionsBuilder.Options);
    }
}