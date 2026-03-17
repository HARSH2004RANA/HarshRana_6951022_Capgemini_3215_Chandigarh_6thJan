using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
	public class Room
	{
		public int Id { get; set; }

		public int RoomNumber { get; set; }

		public string RoomType { get; set; }

		public decimal PricePerDay { get; set; }

		public bool IsOccupied { get; set; }   // ADD THIS
	}
}