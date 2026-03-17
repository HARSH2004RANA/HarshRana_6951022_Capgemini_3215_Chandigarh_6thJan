using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Collections.Generic;

namespace StudentManagement.Controllers
{
	public class StudentController : Controller
	{
		static List<Student> students = new List<Student>();

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(Student student)
		{
			if (ModelState.IsValid)
			{
				student.Id = students.Count + 1;
				students.Add(student);

				TempData["Success"] = "Student Registered Successfully!";
				return RedirectToAction("Details", student);
			}

			return View(student);
		}

		public IActionResult Details(Student student)
		{
			return View(student);
		}
	}
}