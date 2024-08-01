using Microsoft.EntityFrameworkCore;
using ProjectStore.Domen.Models;
using ProjectStore.Infrastructure.Configurations;

namespace ProjectStore.Infrastructure.Data;

public  class ProjectStoreContext : DbContext
{
    public DbSet<Repository> Repositories { get; set; }

    public ProjectStoreContext(DbContextOptions<ProjectStoreContext> opt) : base(opt)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated(); 
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SportStoreBlazor;Trusted_Connection=True;");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RepositoryConfig());
    }
}