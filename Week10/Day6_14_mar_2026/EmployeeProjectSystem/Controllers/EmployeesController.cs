using EmployeeProjectSystem.Data;
using EmployeeProjectSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProjectSystem.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EmployeesController(ApplicationDbContext context)
		{
			_context = context;
		}

		
		public IActionResult Index()
		{
			var employees = _context.Employees
				.Include(e => e.Department)
				.Include(e => e.EmployeeProjects)
				.ThenInclude(ep => ep.Project)
				.ToList();

			return View(employees);
		}

		public IActionResult Create()
		{
			ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name");
			ViewBag.Projects = _context.Projects.ToList();

			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee employee, int[] projectIds)
		{
			_context.Employees.Add(employee);
			_context.SaveChanges();

			// Assign projects
			foreach (var pid in projectIds)
			{
				var empProj = new EmployeeProject
				{
					EmployeeId = employee.EmployeeId,
					ProjectId = pid,
					AssignedDate = DateTime.Now
				};

				_context.EmployeeProjects.Add(empProj);
			}

			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		
		public IActionResult Edit(int id)
		{
			var employee = _context.Employees.Find(id);

			ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
			ViewBag.Projects = _context.Projects.ToList();

			return View(employee);
		}

		[HttpPost]
		public IActionResult Edit(Employee employee)
		{
			_context.Employees.Update(employee);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		
		public IActionResult Delete(int id)
		{
			var emp = _context.Employees
				.Include(e => e.Department)
				.FirstOrDefault(e => e.EmployeeId == id);

			return View(emp);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var emp = _context.Employees.Find(id);

			var empProjects = _context.EmployeeProjects
				.Where(ep => ep.EmployeeId == id);

			_context.EmployeeProjects.RemoveRange(empProjects);
			_context.Employees.Remove(emp);

			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}