using System.ComponentModel.DataAnnotations;
namespace EventBooking.API.Models;
public class Event
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    [FutureDate]
    public DateTime Date { get; set; }

    public string Location { get; set; }

    public int AvailableSeats { get; set; }
}