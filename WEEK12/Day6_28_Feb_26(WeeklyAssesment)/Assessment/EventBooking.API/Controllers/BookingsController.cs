using EventBooking.API.DTOs;
using EventBooking.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EventBooking.API.Helpers;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(
        IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // POST api/bookings

    [HttpPost]
    public async Task<IActionResult>
        BookEvent(BookingDto dto)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        var result =
            await _bookingService
                .BookEventAsync(dto, userId);

        if (result == "Event not found")
            return NotFound(result);

        if (result == "Not enough seats available")
            return BadRequest(result);

        return Ok(result);
    }

    // DELETE api/bookings/{id}

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var result = await _bookingService.CancelBookingAsync(id);

        if (result == "Booking not found")
        {
            return NotFound(new ApiResponse(
                404,
                "Booking not found",
                null
            ));
        }

        return Ok(new ApiResponse(
            200,
            "Booking cancelled successfully",
            result
        ));
    }

    [HttpGet("my-bookings")]
[Authorize]
public async Task<IActionResult>
GetMyBookings()
{
    var userId =
        User.FindFirstValue(
            ClaimTypes.NameIdentifier);

    var bookings =
        await _bookingService
            .GetUserBookingsAsync(userId);

    return Ok(bookings);
}
}