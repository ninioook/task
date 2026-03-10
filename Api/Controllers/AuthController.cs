using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication2.Controllers;

public class AuthController : ControllerBase
{
    private readonly string _jwtSecret = "super-secret-key-change-this";
    private readonly string _jwtIssuer = "myapp";
    private readonly string _jwtAudience = "myapp-users";


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (model.UserName != "admin" || model.Password != "password")
            return Unauthorized();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: new[] { new Claim(ClaimTypes.Name, model.UserName) },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = jwtToken });
    }
    
    [HttpGet("authorize")]
    [Authorize]
    public IActionResult AuthorizeEndpoint()
    {
        return Ok($"Hello {User.Identity?.Name}, you are authorized!");
    }
}