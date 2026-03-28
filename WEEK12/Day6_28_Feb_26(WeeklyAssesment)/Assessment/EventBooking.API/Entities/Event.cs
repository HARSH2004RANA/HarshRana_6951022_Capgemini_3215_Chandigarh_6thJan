using System.ComponentModel.DataAnnotations;
using EventBooking.API.Validators;

namespace EventBooking.API.Entities;

public class Event
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [FutureDate]
    public DateTime Date { get; set; }

    public string Location { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int AvailableSeats { get; set; }

    public ICollection<Booking> Bookings { get; set; }
        = new List<Booking>();
}