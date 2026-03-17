using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
	public class BookingController : Controller
	{
		private readonly HotelDbContext _context;

		public BookingController(HotelDbContext context)
		{
			_context = context;
		}

		// Show all bookings
		public IActionResult Index()
		{
			var bookings = _context.Bookings
				.Include(b => b.Customer)
				.Include(b => b.Room)
				.ToList();

			return View(bookings);
		}

		// Show create booking page
		public IActionResult Create()
		{
			ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name");
			ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "RoomNumber");

			return View();
		}

		// Create booking
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Booking booking)
		{
			if (ModelState.IsValid)
			{
				var room = _context.Rooms.Find(booking.RoomId);

				if (room != null)
				{
					room.IsOccupied = true;
				}

				_context.Bookings.Add(booking);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name", booking.CustomerId);
			ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "RoomNumber", booking.RoomId);

			return View(booking);
		}
	}
}