using Customer___products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class OrdersController : Controller
{
	private readonly AppDBContext _context;

	public OrdersController(AppDBContext context)
	{
		_context = context;
	}

	// READ
	public IActionResult Index()
	{
		var orders = _context.Orders
			.Include(o => o.Customer)
			.Include(o => o.Product)
			.ToList();

		return View(orders);
	}

	// CREATE
	public IActionResult Create()
	{
		ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name");
		ViewBag.Products = new SelectList(_context.Products, "Id", "Name");

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Order order)
	{
		if (ModelState.IsValid)
		{
			_context.Orders.Add(order);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		// Reload dropdowns if validation fails
		ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name", order.CustomerId);
		ViewBag.Products = new SelectList(_context.Products, "Id", "Name", order.ProductId);

		return View(order);
	}

	// DELETE
	public IActionResult Delete(int id)
	{
		var order = _context.Orders
			.Include(o => o.Customer)
			.Include(o => o.Product)
			.FirstOrDefault(o => o.Id == id);

		if (order == null)
		{
			return NotFound();
		}

		return View(order);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public IActionResult DeleteConfirmed(int id)
	{
		var order = _context.Orders.Find(id);

		if (order != null)
		{
			_context.Orders.Remove(order);
			_context.SaveChanges();
		}

		return RedirectToAction(nameof(Index));
	}
}