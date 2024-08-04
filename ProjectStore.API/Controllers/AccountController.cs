using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStore.Domen.Models;
using ProjectStore.Infrastructure.Data;

namespace ProjectStore.API.Controllers;

public class AccountController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtHandler _jwtHandler;
    
    public AccountController(
        ApplicationContext context,
        UserManager<ApplicationUser> userManager,
        JwtHandler jwtHandler)
    {
        _context = context;
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.Email);
        if (user == null
            || !await _userManager.CheckPasswordAsync(user, loginRequest.
                Password))
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