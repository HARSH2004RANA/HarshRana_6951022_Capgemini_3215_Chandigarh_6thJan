using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductApiCache.Data;
using ProductApiCache.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProductApiCache.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (username != "admin" || password != "1234")
                return Unauthorized("Invalid credentials");

            var key = Encoding.UTF8.GetBytes("THIS_IS_A_SUPER_SECRET_KEY_12345678912345");

            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, username)
                },
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}