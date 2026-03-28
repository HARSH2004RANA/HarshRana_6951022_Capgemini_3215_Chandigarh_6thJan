using Microsoft.AspNetCore.Mvc;
using EcommerceLoggingAPI.Models;
using log4net;

namespace EcommerceLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(UserController));

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                log.Info($"Login attempt: {model.Email}");

                if (model.Password != "123456")
                {
                    log.Warn("Invalid password");
                    return Unauthorized("Invalid login");
                }

                return Ok("Login Successful");
            }
            catch (Exception ex)
            {
                log.Error("Login exception", ex);
                return StatusCode(500);
            }
        }
    }
}