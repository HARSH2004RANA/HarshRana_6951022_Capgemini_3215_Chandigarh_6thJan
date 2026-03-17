using Microsoft.AspNetCore.Mvc;
using MVCDemoCourse.Data;
using MVCDemoCourse.Models;
using System.Linq;

namespace MVCDemoBook_ASP.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public IActionResult Index()
        {
            var books = _context.books.ToList();
            return View(books);
        }

        // GET: Book/Details/5
        public IActionResult Details(int id)
        {
            var book = _context.books.Find(id);
            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public IActionResult Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _context.books.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _context.books.Find(id);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public IActionResult Edit(BookModel book)
        {
            _context.books.Update(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int id)
        {
            var book = _context.books.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _context.books.Find(id);

            if (data != null)
            {
                _context.books.Remove(data);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}