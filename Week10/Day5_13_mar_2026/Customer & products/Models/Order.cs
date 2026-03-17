using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Customer___products.Models
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int CustomerId { get; set; }

		[ValidateNever]
		public Customer Customer { get; set; }

		[Required]
		public int ProductId { get; set; }

		[ValidateNever]
		public Product Product { get; set; }
	}
}