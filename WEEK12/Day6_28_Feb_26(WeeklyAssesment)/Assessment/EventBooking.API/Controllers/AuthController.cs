using EventBooking.API.DTOs.Auth;
using EventBooking.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtService _jwtService;

    public AuthController(
        UserManager<IdentityUser> userManager,
        JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    // REGISTER USER

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto)
    {
        var user =
            new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

        var result =
            await _userManager
                .CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Assign default role

        await _userManager
            .AddToRoleAsync(user, "User");

        return Ok("User registered successfully");
    }

    // LOGIN USER

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var user =
            await _userManager
                .FindByNameAsync(dto.Username);

        if (user == null)
            return Unauthorized("Invalid username");

        var valid =
            await _userManager
                .CheckPasswordAsync(
                    user,
                    dto.Password);

        if (!valid)
            return Unauthorized("Invalid password");

        var token = await _jwtService.GenerateToken(
                    user,
                    _userManager);

        return Ok(new
        {
            token
        });
    }
}