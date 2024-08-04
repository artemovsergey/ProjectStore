using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectStore.Domen.Models;

namespace ProjectStore.Infrastructure.Data;

public class JwtHandler
{
    private readonly IConfiguration _configuration;
    
    public JwtHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    public async Task<JwtSecurityToken> GetTokenAsync(ApplicationUser user)
    {
        var jwtOptions = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: await GetClaimsAsync(user),
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(
                _configuration["Jwt:ExpirationTimeInMinutes"])),
            signingCredentials: GetSigningCredentials());
        return jwtOptions;
    }
    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret,
            SecurityAlgorithms.HmacSha256);
    }
    
    private async Task<List<Claim>> GetClaimsAsync(
        ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email)
        };

        var roles = new List<string>() { "Администратор", "Менеджен" };
        
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }

}
