using BooksDemo.Data.DataAccess;
using BooksDemo.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksDemo.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private BooksDBContext _booksDBContext;

        public BooksRepository(BooksDBContext booksDBContext)
        {
            _booksDBContext = booksDBContext;
        }       

        public async Task CreateBook(BookEntity bookEntity)
        {
            _booksDBContext.Attach(bookEntity);
            _booksDBContext.Books.Add(bookEntity);
            _booksDBContext.Entry(bookEntity).State = EntityState.Added;
            var result = await _booksDBContext.SaveChangesAsync();
        }

        public List<BookEntity> GetAllBooks()
        {
            return _booksDBContext.Books.ToList();
        }

        public BookEntity GetBookById(Guid bookId)
        {
           return _booksDBContext.Books.Where(book => book.Id == bookId).FirstOrDefault();
        }

        public async Task UpdateBookAsync(BookEntity bookEntity)
        {
            var existingBook = _booksDBContext.Books.FirstOrDefault(b => b.Id == bookEntity.Id);
            if (existingBook != null)
            {
                _booksDBContext.Entry(existingBook).State = EntityState.Detached;
                _booksDBContext.Attach(bookEntity);
                _booksDBContext.Entry(bookEntity).State = EntityState.Modified;
                var result = await _booksDBContext.SaveChangesAsync();
            }
        }
    }
}