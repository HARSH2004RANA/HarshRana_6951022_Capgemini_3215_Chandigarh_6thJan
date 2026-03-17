using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Customer___products.Models
{
	[Table("Customers")]
	public class Customer
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Customer name is required")]
		public string Name { get; set; } = string.Empty;

		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string? Email { get; set; }

		[Phone(ErrorMessage = "Invalid phone number")]
		public string? Phone { get; set; }

		[ValidateNever]
		public ICollection<Order>? Orders { get; set; }
	}
}