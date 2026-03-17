using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HotelManagementSystem.Models
{
	public class Booking
	{
		public int Id { get; set; }

		[Required]
		public int CustomerId { get; set; }

		[Required]
		public int RoomId { get; set; }

		public int Days { get; set; }

		public int Food { get; set; }

		public int Vehicles { get; set; }

		[ValidateNever]
		public Customer Customer { get; set; }

		[ValidateNever]
		public Room Room { get; set; }
	}
}