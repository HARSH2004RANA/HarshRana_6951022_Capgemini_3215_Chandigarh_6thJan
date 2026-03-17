using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookAndAuthor.Models
{
	public class Book
	{
		public int BookId { get; set; }

		[Required]
		public string Title { get; set; }

		[ValidateNever]
		public ICollection<AuthorBook> AuthorBooks { get; set; }
	}
}