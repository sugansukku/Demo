using BooksDemo.Data.DataAccess;

namespace BooksDemo.Repository.Interface
{
    public interface IBooksRepository
    {
        List<BookEntity> GetAllBooks();
        BookEntity GetBookById(Guid bookId);
        Task CreateBook(BookEntity book);
        Task UpdateBookAsync(BookEntity book);
    }
}
