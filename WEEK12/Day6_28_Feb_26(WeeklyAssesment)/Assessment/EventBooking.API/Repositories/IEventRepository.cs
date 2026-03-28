using EventBooking.API.Entities;

namespace EventBooking.API.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();

    Task<Event?> GetByIdAsync(int id);

    Task AddAsync(Event ev);

    Task UpdateAsync(Event ev);

    Task DeleteAsync(Event ev);

    Task SaveChangesAsync();
}