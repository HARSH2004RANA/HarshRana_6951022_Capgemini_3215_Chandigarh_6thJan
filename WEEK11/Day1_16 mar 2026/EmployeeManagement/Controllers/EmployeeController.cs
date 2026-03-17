using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
		private static List<Employee> employees = new List<Employee>();

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(Employee emp)
		{
			if (ModelState.IsValid)
			{
				emp.Id = employees.Count + 1;
				employees.Add(emp);

				TempData["Success"] = "Employee Registered Successfully!";
				return RedirectToAction("Details", emp);
			}

			return View(emp);
		}

		public IActionResult Details(Employee emp)
		{
			return View(emp);
		}
	}
}
