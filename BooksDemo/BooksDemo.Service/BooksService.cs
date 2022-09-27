using AutoMapper;
using BooksDemo.Data.DataAccess;
using BooksDemo.Models;
using BooksDemo.Repository.Interface;
using BooksDemo.Service.Interface;

namespace BooksDemo.Service
{
    public class BooksService: IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        private IMapper _mapper;
        public BooksService(IBooksRepository bookRepository, IMapper mapper)
        {
            _booksRepository = bookRepository;
            _mapper = mapper;
        }
        public List<Book> GetAllBooks()
        {
            var result = _booksRepository.GetAllBooks();
            return _mapper.Map<List<Book>>(result);
        }
        public Book GetBookById(Guid bookId)
        {
            var result =  _booksRepository.GetBookById(bookId);
            return _mapper.Map<Book>(result);
        }
        public Task CreateBook(BookRequest book)
        {
            var bookEntity = _mapper.Map<BookEntity>(book);
            bookEntity.Id = new Guid();
            return _booksRepository.CreateBook(bookEntity);
        }
        public Task UpdateBookAsync(Book book)
        {
            var bookEntity = _mapper.Map<BookEntity>(book);
            return _booksRepository.UpdateBookAsync(bookEntity);
        }

    }
}