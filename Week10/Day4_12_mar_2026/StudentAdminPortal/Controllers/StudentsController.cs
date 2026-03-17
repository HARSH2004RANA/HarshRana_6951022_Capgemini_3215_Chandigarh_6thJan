using Microsoft.AspNetCore.Mvc;

public class StudentsController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}