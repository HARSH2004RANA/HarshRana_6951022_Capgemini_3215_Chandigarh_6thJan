using AutoMapper;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;
using EventBooking.API.Repositories;

namespace EventBooking.API.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepo;
    private readonly IMapper _mapper;

    public EventService(
        IEventRepository eventRepo,
        IMapper mapper)
    {
        _eventRepo = eventRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventDto>>
        GetAllEventsAsync()
    {
        var events =
            await _eventRepo.GetAllAsync();

        return _mapper
            .Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto?>
        GetEventByIdAsync(int id)
    {
        var ev =
            await _eventRepo.GetByIdAsync(id);

        if (ev == null)
            return null;

        return _mapper.Map<EventDto>(ev);
    }

    public async Task<EventDto>
        CreateEventAsync(EventDto dto)
    {
        var ev =
            _mapper.Map<Event>(dto);

        await _eventRepo.AddAsync(ev);

        await _eventRepo.SaveChangesAsync();

        return _mapper.Map<EventDto>(ev);
    }

    public async Task<bool>
        UpdateEventAsync(int id, EventDto dto)
    {
        var ev =
            await _eventRepo.GetByIdAsync(id);

        if (ev == null)
            return false;

        _mapper.Map(dto, ev);

        await _eventRepo.UpdateAsync(ev);

        await _eventRepo.SaveChangesAsync();

        return true;
    }

    public async Task<bool>
        DeleteEventAsync(int id)
    {
        var ev =
            await _eventRepo.GetByIdAsync(id);

        if (ev == null)
            return false;

        await _eventRepo.DeleteAsync(ev);

        await _eventRepo.SaveChangesAsync();

        return true;
    }
}