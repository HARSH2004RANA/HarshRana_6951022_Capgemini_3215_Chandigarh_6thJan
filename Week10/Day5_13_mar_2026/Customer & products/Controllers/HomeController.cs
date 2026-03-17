using Customer___products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
	private readonly AppDBContext _context;

	public HomeController(AppDBContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{
		var orders = _context.Orders
			.Include(o => o.Customer)
			.Include(o => o.Product)
			.ToList();

		return View(orders);
	}
}