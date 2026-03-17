using Microsoft.AspNetCore.Mvc;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IRequestLogService _logService;

        public StudentsController(IRequestLogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            var students = new List<string>
        {
            "Harsh",
            "Aman",
            "Rohit"
        };

            ViewBag.Logs = _logService.GetLogs();

            return View(students);
        }
    }
}
