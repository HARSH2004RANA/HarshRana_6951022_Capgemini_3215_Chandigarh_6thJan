using EventBooking.Web.Models;
using EventBooking.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages;

public class EventsModel : PageModel
{
    private readonly ApiService _apiService;

    public EventsModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<EventDto> Events { get; set; }

    public async Task OnGetAsync()
    {
        Events =
            await _apiService
                .GetAsync<EventDto>(
                    "api/events");
    }
}