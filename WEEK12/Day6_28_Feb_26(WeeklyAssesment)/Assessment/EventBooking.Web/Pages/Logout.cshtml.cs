using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnPost()
    {
        // Remove the JWT token from session
        HttpContext.Session.Remove("JWToken");
        
        // Redirect to home page
        return RedirectToPage("/Index");
    }
}
