using BooksDemo.Models;

namespace BooksDemo.Service.Interface
{
    public interface IBooksService
    {
        List<Book> GetAllBooks();
        Book GetBookById(Guid bookId);
        Task CreateBook(BookRequest book);
        Task UpdateBookAsync(Book book);
    }
}
