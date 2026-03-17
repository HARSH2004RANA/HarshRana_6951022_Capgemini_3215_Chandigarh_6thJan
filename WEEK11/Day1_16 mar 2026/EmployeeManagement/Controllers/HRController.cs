using EmployeeManagement.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
	[ServiceFilter(typeof(LogActionFilter))]
	public class HRController : Controller
    {
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult EmployeeList()
		{
			return View();
		}

		public IActionResult Reports()
		{
			// Testing Exception Filter
			throw new Exception("Something went wrong in Reports!");
		}
	}
}
