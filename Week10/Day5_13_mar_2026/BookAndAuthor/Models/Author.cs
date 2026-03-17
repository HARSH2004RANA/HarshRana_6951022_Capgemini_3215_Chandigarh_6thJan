using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookAndAuthor.Models
{
	public class Author
	{
		public int AuthorId { get; set; }

		[Required]
		public string Name { get; set; }
		[ValidateNever]
		public ICollection<AuthorBook> AuthorBooks { get; set; }
	}
}