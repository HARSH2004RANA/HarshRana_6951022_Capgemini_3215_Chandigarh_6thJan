using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
	public class Customer
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Age is required")]
		public int Age { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		public string Phone { get; set; }

		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
	}
}