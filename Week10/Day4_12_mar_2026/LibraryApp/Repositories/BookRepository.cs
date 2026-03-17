using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Book> books = new List<Book>
    {
        new Book { Id = 1, Title = "Clean Code", Author = "Robert Martin"},
        new Book { Id = 2, Title = "Design Patterns", Author = "GoF"},
        new Book { Id = 3, Title = "Data Structures", Author = "Mark Allen"}
    };

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }
    }
}
