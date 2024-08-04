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
    public ActionResult CreateDefaultUsers()
    {
        return Ok();
    }

}