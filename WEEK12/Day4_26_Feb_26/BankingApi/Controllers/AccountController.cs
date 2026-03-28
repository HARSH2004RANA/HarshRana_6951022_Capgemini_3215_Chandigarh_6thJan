using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BankingApi.Data;
using BankingApi.DTOs;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AccountController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("details")]
    public async Task<IActionResult> GetAccountDetails()
    {
        var email = User.Identity?.Name;

        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.UserEmail == email);

        if (account == null)
            return NotFound();

        var result = _mapper.Map<AccountDTO>(account);

        return Ok(result);
    }
}