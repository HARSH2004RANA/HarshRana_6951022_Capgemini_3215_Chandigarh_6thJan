using AutoMapper;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;
using EventBooking.API.Repositories;

namespace EventBooking.API.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepo;
    private readonly IEventRepository _eventRepo;
    private readonly IMapper _mapper;

    public BookingService(
        IBookingRepository bookingRepo,
        IEventRepository eventRepo,
        IMapper mapper)
    {
        _bookingRepo = bookingRepo;
        _eventRepo = eventRepo;
        _mapper = mapper;
    }

    public async Task<string> BookEventAsync(
        BookingDto dto,
        string userId)
    {
        var ev =
            await _eventRepo
                .GetByIdAsync(dto.EventId);

        if (ev == null)
            return "Event not found";

        if (ev.AvailableSeats < dto.SeatsBooked)
            return "Not enough seats available";

        ev.AvailableSeats -= dto.SeatsBooked;

        var booking =
            _mapper.Map<Booking>(dto);

        booking.UserId = userId;

        await _bookingRepo.AddAsync(booking);

        await _eventRepo.SaveChangesAsync();
        await _bookingRepo.SaveChangesAsync();

        return "Booking successful";
    }

    public async Task<string> CancelBookingAsync(
        int bookingId)
    {
        var booking =
            await _bookingRepo
                .GetByIdAsync(bookingId);

        if (booking == null)
            return "Booking not found";

        var ev =
            await _eventRepo
                .GetByIdAsync(booking.EventId);

        if (ev != null)
        {
            ev.AvailableSeats +=
                booking.SeatsBooked;
        }

        await _bookingRepo.DeleteAsync(booking);

        await _bookingRepo.SaveChangesAsync();

        return "Booking cancelled";
    }
    public async Task<IEnumerable<BookingResponseDto>>
    GetUserBookingsAsync(string userId)
{
    var bookings =
        await _bookingRepo
            .GetUserBookingsAsync(userId);

    return bookings.Select(b =>
        new BookingResponseDto
        {
            BookingId = b.Id,
            EventId = b.EventId,
            EventTitle = b.Event.Title,
            EventDate = b.Event.Date,
            Location = b.Event.Location,
            SeatsBooked = b.SeatsBooked
        });
}
}