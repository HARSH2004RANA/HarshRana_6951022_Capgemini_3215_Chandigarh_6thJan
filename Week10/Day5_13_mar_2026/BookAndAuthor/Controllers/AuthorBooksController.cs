using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookAndAuthor.Data;
using BookAndAuthor.Models;

namespace BookAndAuthor.Controllers
{
	public class AuthorBooksController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AuthorBooksController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: AuthorBooks
		public async Task<IActionResult> Index()
		{
			var authorBooks = _context.AuthorBooks
				.Include(a => a.Author)
				.Include(a => a.Book);

			return View(await authorBooks.ToListAsync());
		}

		// GET: AuthorBooks/Create
		public IActionResult Create()
		{
			ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name");
			ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title");

			return View();
		}

		// POST: AuthorBooks/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("AuthorId,BookId")] AuthorBook authorBook)
		{
			
				_context.AuthorBooks.Add(authorBook);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			

			ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name", authorBook.AuthorId);
			ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", authorBook.BookId);

			return View(authorBook);
		}

		// GET: AuthorBooks/Edit
		public async Task<IActionResult> Edit(int authorId, int bookId)
		{
			var authorBook = await _context.AuthorBooks
				.FirstOrDefaultAsync(ab => ab.AuthorId == authorId && ab.BookId == bookId);

			if (authorBook == null)
			{
				return NotFound();
			}

			ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name", authorBook.AuthorId);
			ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", authorBook.BookId);

			return View(authorBook);
		}

		// POST: AuthorBooks/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int authorId, int bookId, [Bind("AuthorId,BookId")] AuthorBook authorBook)
		{
			if (authorId != authorBook.AuthorId || bookId != authorBook.BookId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(authorBook);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.AuthorBooks.Any(e => e.AuthorId == authorId && e.BookId == bookId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				return RedirectToAction(nameof(Index));
			}

			return View(authorBook);
		}

		// GET: AuthorBooks/Delete
		public async Task<IActionResult> Delete(int authorId, int bookId)
		{
			var authorBook = await _context.AuthorBooks
				.Include(a => a.Author)
				.Include(a => a.Book)
				.FirstOrDefaultAsync(m => m.AuthorId == authorId && m.BookId == bookId);

			if (authorBook == null)
			{
				return NotFound();
			}

			return View(authorBook);
		}

		// POST: AuthorBooks/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int authorId, int bookId)
		{
			var authorBook = await _context.AuthorBooks
				.FirstOrDefaultAsync(m => m.AuthorId == authorId && m.BookId == bookId);

			if (authorBook != null)
			{
				_context.AuthorBooks.Remove(authorBook);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}