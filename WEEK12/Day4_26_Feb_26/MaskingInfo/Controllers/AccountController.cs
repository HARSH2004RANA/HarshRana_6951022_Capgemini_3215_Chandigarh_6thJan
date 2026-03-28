using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MaskingInfo.Services;
using MaskingInfo.DTOs;
using MaskingInfo.Models;

namespace MaskingInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AccountService _service;

        public AccountController(IMapper mapper, AccountService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("masked")]
        public IActionResult GetMaskedAccounts()
        {
            var accounts = _service.GetAccounts();

            var result = _mapper.Map<List<AccountDTO>>(accounts);

            return Ok(result);
        }
    }
}