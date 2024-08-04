using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStore.Domen.Models;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public SeedController(
        ApplicationContext context,
        IWebHostEnvironment env,
        IConfiguration configuration)
    {
        _context = context;
        _env = env;
        _configuration = configuration;
    }


    [HttpGet]
    public async Task<ActionResult> CreateDefaultUsers()
    {
        string message;
        var user = new ApplicationUser() { Email = "user@email.com", Password = "123" };
        try
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            message = e.InnerException.ToString();
            Console.WriteLine(e.InnerException);
            return BadRequest(message);
        }
        
        return Ok($"Пользователь email: {user.Email} password: {user.Password}");
    }

}