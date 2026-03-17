using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
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
			if (username == "admin" && password == "admin123")
			{
				HttpContext.Session.SetString("User", username);
				HttpContext.Session.SetString("Role", "Admin");

				return RedirectToAction("Dashboard");
			}

			ViewBag.Error = "Invalid Credentials";
			return View();
		}

		public IActionResult Dashboard()
		{
			var user = HttpContext.Session.GetString("User");

			if (user == null)
				return RedirectToAction("Login");

			ViewBag.User = user;
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
