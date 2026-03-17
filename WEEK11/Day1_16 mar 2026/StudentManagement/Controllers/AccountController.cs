using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
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
			if (username == "admin" && password == "123")
			{
				HttpContext.Session.SetString("Username", username);
				return RedirectToAction("Dashboard");
			}

			ViewBag.Error = "Invalid Username or Password";
			return View();
		}

		public IActionResult Dashboard()
		{
			var user = HttpContext.Session.GetString("Username");

			if (string.IsNullOrEmpty(user))
				return RedirectToAction("Login");

			ViewBag.Username = user;
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}