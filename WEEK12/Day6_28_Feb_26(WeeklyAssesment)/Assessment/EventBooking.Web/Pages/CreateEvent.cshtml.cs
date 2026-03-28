using EventBooking.Web.Models;
using EventBooking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages;

public class CreateEventModel : PageModel
{
    private readonly ApiService _apiService;

    public CreateEventModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public EventDto Event { get; set; }

    public string ErrorMessage { get; set; }
    public string SuccessMessage { get; set; }

    public void OnGet()
    {
        Event = new EventDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Please fill in all required fields correctly.";
            return Page();
        }

        try
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            var response = await _apiService.PostAsync("api/events", Event, token);

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Failed to create event. Please try again.";
                return Page();
            }

            SuccessMessage = "Event created successfully!";
            Event = new EventDto();
            return RedirectToPage("/Events");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
            return Page();
        }
    }
}
