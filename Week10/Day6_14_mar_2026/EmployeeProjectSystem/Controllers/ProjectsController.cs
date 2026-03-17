using EmployeeProjectSystem.Data;
using EmployeeProjectSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProjectSystem.Controllers
{
	public class ProjectsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProjectsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// INDEX
		public IActionResult Index()
		{
			var projects = _context.Projects
				.Include(p => p.EmployeeProjects)
				.ThenInclude(ep => ep.Employee)
				.ToList();

			return View(projects);
		}

		// CREATE
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Project project)
		{
			_context.Projects.Add(project);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		// EDIT
		public IActionResult Edit(int id)
		{
			var project = _context.Projects.Find(id);
			return View(project);
		}

		[HttpPost]
		public IActionResult Edit(Project project)
		{
			_context.Projects.Update(project);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		// DELETE
		public IActionResult Delete(int id)
		{
			var project = _context.Projects.Find(id);
			return View(project);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var project = _context.Projects.Find(id);

			_context.Projects.Remove(project);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}