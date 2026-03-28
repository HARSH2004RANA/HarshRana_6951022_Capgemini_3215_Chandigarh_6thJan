namespace EventBooking.Web.Models;

public class BookingResponseDto
{
    public int BookingId { get; set; }

    public int EventId { get; set; }

    public string EventTitle { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; }

    public int SeatsBooked { get; set; }
}
