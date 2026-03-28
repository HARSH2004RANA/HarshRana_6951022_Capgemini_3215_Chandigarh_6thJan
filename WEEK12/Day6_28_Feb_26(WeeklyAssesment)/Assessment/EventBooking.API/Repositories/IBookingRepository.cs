using EventBooking.API.Entities;

namespace EventBooking.API.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(int id);

    Task<IEnumerable<Booking>>
        GetUserBookingsAsync(string userId);

    Task AddAsync(Booking booking);

    Task DeleteAsync(Booking booking);

    Task SaveChangesAsync();
}