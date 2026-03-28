using EventBooking.Web.Models;
using EventBooking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace EventBooking.Web.Pages;

public class BookEventModel : PageModel
{
    private readonly ApiService _apiService;

    public BookEventModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public BookingDto Booking { get; set; }

    public int EventId { get; set; }

    public string Message { get; set; }

    public void OnGet(int id)
    {
        EventId = id;

        Booking = new BookingDto
        {
            EventId = id
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var token =
            HttpContext.Session
                .GetString("JWToken");

        var response =
            await _apiService.PostAsync(
                "api/bookings",
                Booking,
                token);

        if (response.IsSuccessStatusCode)
        {
            Message = "Booking successful!";
            return Page();
        }

        Message = "Booking failed";

        return Page();
    }
}