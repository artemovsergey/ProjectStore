using Microsoft.EntityFrameworkCore;
using ProjectStore.Domen.Models;

namespace ProjectStore.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public DbSet<ApplicationUser> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
    {
        //Database.EnsureDeleted();
        Database.Migrate();
    }
}