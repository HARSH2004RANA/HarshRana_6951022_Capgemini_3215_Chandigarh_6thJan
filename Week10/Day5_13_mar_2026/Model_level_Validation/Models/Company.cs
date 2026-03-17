using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Model_level_Validation.Models
{
	public class Company
	{
		[Key]
		public int CompanyId { get; set; }

		[Required(ErrorMessage = "Company Name is required")]
		[StringLength(100)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Location is required")]
		public string Location { get; set; }

		// Navigation Property
		[ValidateNever]
		public ICollection<Employee> Employees { get; set; }
	}
}