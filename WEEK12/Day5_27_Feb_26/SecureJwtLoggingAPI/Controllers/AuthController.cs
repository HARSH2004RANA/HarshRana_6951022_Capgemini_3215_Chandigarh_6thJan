using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using log4net;
using SecureJwtLoggingAPI.Models;

namespace SecureJwtLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(AuthController));

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (model.Username != "admin"
                    || model.Password != "123")
                {
                    log.Warn("Failed login attempt");

                    return Unauthorized();
                }

                var token =
                    GenerateToken(model.Username);

                log.Info(
                    $"Token generated for user: {model.Username}");

                return Ok(token);
            }
            catch (Exception ex)
            {
                log.Error("Login exception", ex);

                return StatusCode(500);
            }
        }

        private string GenerateToken(string username)
        {
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        "THIS_IS_MY_SUPER_SECRET_KEY_12345"));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var claims =
                new[]
                {
                    new Claim(
                        ClaimTypes.Name,
                        username)
                };

            var token =
                new JwtSecurityToken(
                    claims: claims,
                    expires:
                        DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}