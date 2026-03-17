using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Data;
using HotelManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
	public class BillingController : Controller
	{
		private readonly HotelDbContext _context;
		private readonly BillingService _billingService;

		public BillingController(HotelDbContext context)
		{
			_context = context;
			_billingService = new BillingService();
		}

		public IActionResult Index(int id)
		{
			var booking = _context.Bookings
				.Include(b => b.Customer)
				.Include(b => b.Room)
				.FirstOrDefault(b => b.Id == id);

			if (booking == null)
			{
				return NotFound();
			}

			int totalBill = _billingService.CalculateBill(
				booking.Days,
				(int)booking.Room.PricePerDay,
				booking.Vehicles,
				booking.Food
			);

			ViewBag.Customer = booking.Customer.Name;
			ViewBag.Room = booking.Room.RoomNumber;
			ViewBag.Days = booking.Days;
			ViewBag.RoomPrice = booking.Room.PricePerDay;
			ViewBag.Vehicles = booking.Vehicles;
			ViewBag.Food = booking.Food;
			ViewBag.TotalBill = totalBill;

			return View();
		}
	}
}