using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
	public class CustomerController : Controller
	{
		private readonly HotelDbContext _context;

		public CustomerController(HotelDbContext context)
		{
			_context = context;
		}

		// Show all customers
		public IActionResult Index()
		{
			var customers = _context.Customers.ToList();
			return View(customers);
		}

		// Show create page
		public IActionResult Create()
		{
			return View();
		}

		// Add customer
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Customer customer)
		{
			if (ModelState.IsValid)
			{
				_context.Customers.Add(customer);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(customer);
		}

		// Delete customer
		public IActionResult Delete(int id)
		{
			var customer = _context.Customers.Find(id);

			if (customer == null)
			{
				return NotFound();
			}

			return View(customer);
		}

		// Confirm delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var customer = _context.Customers
				.Include(c => c.Bookings)
				.FirstOrDefault(c => c.Id == id);

			if (customer != null)
			{
				foreach (var booking in customer.Bookings)
				{
					var room = _context.Rooms.Find(booking.RoomId);

					if (room != null)
					{
						room.IsOccupied = false;
					}

					_context.Bookings.Remove(booking);
				}

				_context.Customers.Remove(customer);
				_context.SaveChanges();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}