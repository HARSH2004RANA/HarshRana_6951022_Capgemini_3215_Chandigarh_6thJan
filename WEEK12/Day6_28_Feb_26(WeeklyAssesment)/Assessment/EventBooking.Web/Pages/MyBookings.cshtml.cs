using EventBooking.Web.Models;
using EventBooking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EventBooking.Web.Pages;

public class MyBookingsModel : PageModel
{
    private readonly ApiService _apiService;

    public MyBookingsModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<BookingResponseDto>
        Bookings { get; set; }

    public async Task OnGetAsync()
    {
        var token =
            HttpContext.Session
                .GetString("JWToken");

        if (string.IsNullOrEmpty(token))
        {
            Response.Redirect("/Login");
            return;
        }

        Bookings =
            await _apiService
                .GetAsync<BookingResponseDto>(
                    "api/bookings/my-bookings", 
                    token);
    }

    public async Task<IActionResult>
        OnPostCancelAsync(int id)
    {
        var token =
            HttpContext.Session
                .GetString("JWToken");

        await _apiService.DeleteAsync(
            $"api/bookings/{id}",
            token);

        return RedirectToPage();
    }
}