using EventBooking.Web.Models;
using EventBooking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages;

public class RegisterModel : PageModel
{
    private readonly ApiService _apiService;

    public RegisterModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public RegisterDto RegisterDto { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (RegisterDto.Password != RegisterDto.ConfirmPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return Page();
        }

        try
        {
            var result = await _apiService.PostAsync<RegisterDto>(
                "api/auth/register",
                RegisterDto);

            if (result != null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                ErrorMessage = "Registration failed. Please try again.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }

        return Page();
    }
}