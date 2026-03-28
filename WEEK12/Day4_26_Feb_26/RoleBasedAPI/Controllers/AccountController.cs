using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private static Account account = new Account
    {
        Id = 1,
        Name = "Moni",
        Email = "moni@gmail.com",
        Balance = 50000,
        AccountNumber = "1234567890"
    };

    [HttpGet("details")]
    [Authorize]
    public IActionResult GetDetails()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        if (role == "Admin")
        {
            var adminDto = new AdminAccountDTO
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                Balance = account.Balance,
                AccountNumber = account.AccountNumber
            };

            return Ok(adminDto);
        }
        else
        {
            var userDto = new UserAccountDTO
            {
                Name = account.Name,
                Email = account.Email
            };

            return Ok(userDto);
        }
    }
}