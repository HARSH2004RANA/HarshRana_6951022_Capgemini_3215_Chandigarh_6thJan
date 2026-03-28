using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // 🔥 Simple login (hardcoded)
            if (username == "admin" && password == "123")
            {
                HttpContext.Session.SetString("Admin", "true");
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

