using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectStore.Domen.Models;

namespace ProjectStore.Infrastructure.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    
}