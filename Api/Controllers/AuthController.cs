using Core;
using Core.Interfaces;
using Core.QueryHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly string _jwtSecret = "super-secret-key-change-this";
    private readonly string _jwtIssuer = "myapp";
    private readonly string _jwtAudience = "myapp-users";

    private readonly CustomerQueryHandler _customerQueryHandler;

    public AuthController(CustomerQueryHandler customerQueryHandler, IConfiguration config)
    {
        _customerQueryHandler = customerQueryHandler;
        _jwtSecret = config["Jwt:Secret"]
            ?? "this-is-a-super-secret-key-that-is-32-chars!";
        _jwtIssuer = config["Jwt:Issuer"] ?? "myapp";
        _jwtAudience = config["Jwt:Audience"] ?? "myapp-users";
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
    {
        var query = new CheckUserNameQuery { UserName = model.UserName,Password=model.Password };
        var customer = await _customerQueryHandler.Handle(query, cancellationToken);

        if (customer is null)
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
}