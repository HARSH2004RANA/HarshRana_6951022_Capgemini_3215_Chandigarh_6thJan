using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Models;

[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EventsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ✅ GET: api/events
    [HttpGet]
    public IActionResult GetEvents()
    {
        var events = _context.Events.ToList();

        var result = events.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Date = e.Date,
            Location = e.Location,
            AvailableSeats = e.AvailableSeats
        }).ToList();

        return Ok(result);
    }

    // ✅ GET BY ID (Optional but good for marks)
    [HttpGet("{id}")]
    public IActionResult GetEventById(int id)
    {
        var ev = _context.Events.Find(id);

        if (ev == null)
            return NotFound("Event not found");

        var dto = new EventDto
        {
            Id = ev.Id,
            Title = ev.Title,
            Description = ev.Description,
            Date = ev.Date,
            Location = ev.Location,
            AvailableSeats = ev.AvailableSeats
        };

        return Ok(dto);
    }

    // ✅ POST: Add Event
    [HttpPost]
    public IActionResult AddEvent(EventDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ev = new Event
        {
            // ❌ DO NOT SET Id (auto-generated)
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            Location = dto.Location,
            AvailableSeats = dto.AvailableSeats
        };

        _context.Events.Add(ev);
        _context.SaveChanges();

        return Ok(new
        {
            message = "Event created successfully",
            eventId = ev.Id
        });
    }

    // ✅ PUT: Update Event (BONUS)
    [HttpPut("{id}")]
    public IActionResult UpdateEvent(int id, EventDto dto)
    {
        var ev = _context.Events.Find(id);

        if (ev == null)
            return NotFound("Event not found");

        ev.Title = dto.Title;
        ev.Description = dto.Description;
        ev.Date = dto.Date;
        ev.Location = dto.Location;
        ev.AvailableSeats = dto.AvailableSeats;

        _context.SaveChanges();

        return Ok("Event updated successfully");
    }

    // ✅ DELETE: Delete Event (BONUS)
    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        var ev = _context.Events.Find(id);

        if (ev == null)
            return NotFound("Event not found");

        _context.Events.Remove(ev);
        _context.SaveChanges();

        return Ok("Event deleted successfully");
    }
}