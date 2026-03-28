using EventBooking.API.DTOs;

namespace EventBooking.API.Services;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();

    Task<EventDto?> GetEventByIdAsync(int id);

    Task<EventDto> CreateEventAsync(EventDto dto);

    Task<bool> UpdateEventAsync(int id, EventDto dto);

    Task<bool> DeleteEventAsync(int id);
}