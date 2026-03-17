using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Model_level_Validation.Models
{
	public class Employee
	{
		[Key]
		public int EmployeeId { get; set; }

		[Required(ErrorMessage = "Employee Name is required")]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
		public int Age { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Enter valid Email")]
		public string Email { get; set; }

		// Foreign Key
		public int CompanyId { get; set; }

		// Navigation Property
		[ValidateNever]
		public Company Company { get; set; }
	}
}	