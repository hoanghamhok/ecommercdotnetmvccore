using Microsoft.AspNetCore.Mvc;
using MYWEBAPI.Data;
using MYWEBAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace MYWEBAPI.Controllers
{
[ApiController]
[Route("api/auth")]

public class AuthController:Controllers{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;//to read apssettings.json
    public AuthController(AppDbContext context)
    {
        _context = context;
    }
//API Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Unauthorized("Invalid username or password.");
        }
        // Generate JWT token here (omitted for brevity)
        var token = GenerateJwtToken(user); 
        //Return user's informations and token
        return Ok(new { Token = token, User = new { user.UserId, user.Username, user.Email, user.Role } });
    }
    //Generate Token Method
    private String GenerateJwtToken(User user)
    {
        var jwKey = _config["Jwt:Key"]?? throw new InvalidOperationException("JWT key not found in configuration.");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(_config["Jwt:ExpiryInMinutes"]),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}}