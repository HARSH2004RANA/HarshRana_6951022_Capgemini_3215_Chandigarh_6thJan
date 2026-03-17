using Microsoft.AspNetCore.Mvc;

namespace SessionBasedManagement.Controllers
{
    public class AccountController : Controller
    {
		public IActionResult Login()
		{
			return View();
		}

		// POST: Login
		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			if (username == "admin" && password == "123")
			{
				// Store username in session
				HttpContext.Session.SetString("User", username);

				return RedirectToAction("Dashboard");
			}
			else
			{
				ViewBag.Error = "Invalid Username or Password";
				return View();
			}
		}

		public IActionResult Dashboard()
		{
			var user = HttpContext.Session.GetString("User");

			if (user == null)
			{
				return RedirectToAction("Login");
			}

			ViewBag.Username = user;
			return View();
		}

		// Logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
