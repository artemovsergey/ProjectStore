using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly JwtHandler _jwtHandler;
    
    public AccountController(
        ApplicationContext context,
        JwtHandler jwtHandler)
    {
        _context = context;
        _jwtHandler = jwtHandler;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _context.Users.Where(u => u.Email == loginRequest.Email).FirstOrDefaultAsync();
        var password = await _context.Users.Where(u => u.Password == loginRequest.Password).FirstOrDefaultAsync();
        
        if (user == null
            || password == null)
            return Unauthorized(new LoginResult() {
                Success = false,
                Message = "Invalid Email or Password."
            });
        var secToken = await _jwtHandler.GetTokenAsync(user);
        var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
        return Ok(new LoginResult() {
            Success = true, Message = "Login successful", Token = jwt
        });
    }
    
}