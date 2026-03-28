using EventBooking.API.DTOs;

namespace EventBooking.API.Services;

public interface IBookingService
{
    Task<string> BookEventAsync(
        BookingDto dto,
        string userId);

    Task<string> CancelBookingAsync(
        int bookingId);

    Task<IEnumerable<BookingResponseDto>>
    GetUserBookingsAsync(string userId);
}