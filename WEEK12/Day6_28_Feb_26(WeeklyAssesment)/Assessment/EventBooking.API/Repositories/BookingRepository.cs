using EventBooking.API.Data;
using EventBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Booking>>
        GetUserBookingsAsync(string userId)
    {
        return await _context.Bookings
            .Include(b => b.Event)
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task DeleteAsync(Booking booking)
    {
        _context.Bookings.Remove(booking);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}