using EmployeeProjectSystem.Data;
using EmployeeProjectSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProjectSystem.Controllers
{
	public class DepartmentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public DepartmentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// LIST
		public IActionResult Index()
		{
			var departments = _context.Departments.ToList();
			return View(departments);
		}

		// CREATE
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department department)
		{
			_context.Departments.Add(department);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		// EDIT
		public IActionResult Edit(int id)
		{
			var dept = _context.Departments.Find(id);
			return View(dept);
		}

		[HttpPost]
		public IActionResult Edit(Department department)
		{
			_context.Departments.Update(department);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		// DELETE
		public IActionResult Delete(int id)
		{
			var dept = _context.Departments.Find(id);
			return View(dept);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var dept = _context.Departments.Find(id);
			_context.Departments.Remove(dept);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}