using Microsoft.EntityFrameworkCore;
using BookAndAuthor.Models;

namespace BookAndAuthor.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Author> Authors { get; set; }

		public DbSet<Book> Books { get; set; }

		public DbSet<AuthorBook> AuthorBooks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AuthorBook>()
				.HasKey(ab => new { ab.AuthorId, ab.BookId });
		}
	}
}