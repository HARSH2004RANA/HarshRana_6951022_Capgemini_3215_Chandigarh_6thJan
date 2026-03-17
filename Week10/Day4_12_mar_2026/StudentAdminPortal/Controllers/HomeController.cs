using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.Models;

namespace StudentAdminPortal.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		// GET: /Home/Login
		public IActionResult Login()
		{
			return View();
		}

		// POST: /Home/Login
		[HttpPost]
		public IActionResult Login(string username)
		{
			HttpContext.Session.SetString("User", username);

			return RedirectToAction("Dashboard", "Admin");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}