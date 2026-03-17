using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Data
{
	public class HotelDbContext : DbContext
	{
		public HotelDbContext(DbContextOptions<HotelDbContext> options)
			: base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Servant> Servants { get; set; }
	}
}