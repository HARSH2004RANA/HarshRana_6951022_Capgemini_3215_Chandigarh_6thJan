using AutoMapper;
using EventBooking.API.Entities;
using EventBooking.API.DTOs;

namespace EventBooking.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>()
            .ReverseMap();

        CreateMap<Booking, BookingDto>()
            .ReverseMap();
    }
}