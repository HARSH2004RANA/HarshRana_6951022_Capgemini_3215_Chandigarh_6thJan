using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
	public class ServantController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}
	}
}