using EventBooking.API.DTOs;
using EventBooking.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(
        IEventService eventService)
    {
        _eventService = eventService;
    }

    // GET api/events

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events =
            await _eventService
                .GetAllEventsAsync();

        return Ok(events);
    }

    [HttpGet("check-admin-role")]
    public async Task<IActionResult> CheckAdminRole(
        [FromServices] UserManager<IdentityUser> userManager)
    {
        var user =
            await userManager.FindByNameAsync("admin");

        if (user == null)
            return NotFound("Admin user not found");

        var roles =
            await userManager.GetRolesAsync(user);

        return Ok(roles);
    }

    // GET api/events/{id}

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetEvent(int id)
    {
        var ev =
            await _eventService
                .GetEventByIdAsync(id);

        if (ev == null)
            return NotFound("Event not found");

        return Ok(ev);
    }

    // POST api/events

    [HttpPost]
    [Authorize]
    public async Task<IActionResult>
        CreateEvent(EventDto dto)
    {
        var created =
            await _eventService
                .CreateEventAsync(dto);

        return CreatedAtAction(
            nameof(GetEvent),
            new { id = created.Id },
            created);
    }

    // PUT api/events/{id}

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult>
        UpdateEvent(
            int id,
            EventDto dto)
    {
        var updated =
            await _eventService
                .UpdateEventAsync(id, dto);

        if (!updated)
            return NotFound("Event not found");

        return Ok("Event updated");
    }

    // DELETE api/events/{id}

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult>
        DeleteEvent(int id)
    {
        var deleted =
            await _eventService
                .DeleteEventAsync(id);

        if (!deleted)
            return NotFound("Event not found");

        return Ok("Event deleted");
    }
}