using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Register(Student student)
		{
			if (ModelState.IsValid)
			{
				// Dummy ID generation
				student.Id = new Random().Next(1, 1000);

				TempData["Message"] = "Student Registered Successfully!";

				return RedirectToAction("Details", new { id = student.Id, name = student.Name, age = student.Age });
			}

			return View(student);
		}

		public IActionResult Details(int id, string name, int age)
		{
			Student student = new Student
			{
				Id = id,
				Name = name,
				Age = age
			};

			return View(student);
		}
	}
}
