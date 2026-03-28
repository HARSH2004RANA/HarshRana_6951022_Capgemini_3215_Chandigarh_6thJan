using EventBooking.Web.Models;
using EventBooking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EventBooking.Web.Pages;

public class LoginModel : PageModel
{
    private readonly ApiService _apiService;

    public LoginModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public LoginDto Login { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var response =
            await _apiService.PostAsync(
                "api/auth/login",
                Login);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = "Invalid login";
            return Page();
        }

        var json =
            await response.Content
                .ReadAsStringAsync();

        var tokenObj =
            JsonSerializer.Deserialize<JsonElement>(json);

        var token =
            tokenObj
                .GetProperty("token")
                .GetString();

        HttpContext.Session
            .SetString("JWToken", token);

        return RedirectToPage("/Events");
    }
}