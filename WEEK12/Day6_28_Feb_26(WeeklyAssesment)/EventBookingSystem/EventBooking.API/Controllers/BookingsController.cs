using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Models;

[Route("api/bookings")]
[ApiController]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult BookTickets(BookingDto dto)
    {
        var ev = _context.Events.Find(dto.EventId);

        if (ev == null || ev.AvailableSeats < dto.SeatsBooked)
            return BadRequest("Not enough seats");

        ev.AvailableSeats -= dto.SeatsBooked;

        var booking = new Booking
        {
            EventId = dto.EventId,
            SeatsBooked = dto.SeatsBooked,
            UserId = "User1"
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return Ok("Booking Successful");
    }

    [HttpDelete("{id}")]
    public IActionResult CancelBooking(int id)
    {
        var booking = _context.Bookings.Find(id);

        if (booking == null)
            return NotFound();

        _context.Bookings.Remove(booking);
        _context.SaveChanges();

        return Ok("Booking Cancelled");
    }
}