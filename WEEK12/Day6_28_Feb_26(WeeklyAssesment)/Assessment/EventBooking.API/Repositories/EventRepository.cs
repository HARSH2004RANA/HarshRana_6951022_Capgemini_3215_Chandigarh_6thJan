using EventBooking.API.Data;
using EventBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Event ev)
    {
        await _context.Events.AddAsync(ev);
    }

    public async Task UpdateAsync(Event ev)
    {
        _context.Events.Update(ev);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Event ev)
    {
        _context.Events.Remove(ev);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}