using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using System.Linq;

namespace HotelManagementSystem.Controllers
{
	public class RoomController : Controller
	{
		private readonly HotelDbContext _context;

		public RoomController(HotelDbContext context)
		{
			_context = context;
		}

		// Show all rooms
		public IActionResult Index()
		{
			var rooms = _context.Rooms.ToList();
			return View(rooms);
		}

		// Show create page
		public IActionResult Create()
		{
			return View();
		}

		// Create room
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Room room)
		{
			if (ModelState.IsValid)
			{
				room.IsOccupied = false;
				_context.Rooms.Add(room);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(room);
		}

		// Delete page
		public IActionResult Delete(int id)
		{
			var room = _context.Rooms.Find(id);

			if (room == null)
			{
				return NotFound();
			}

			return View(room);
		}

		// Confirm delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var room = _context.Rooms.Find(id);

			if (room != null)
			{
				if (room.IsOccupied)
				{
					ModelState.AddModelError("", "Cannot delete an occupied room.");
					return View(room);
				}

				_context.Rooms.Remove(room);
				_context.SaveChanges();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}