using BooksDemo.Logging.Interface;
using BooksDemo.Models;
using BooksDemo.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBooksService _booksService;
        private readonly ILog _logger;
        public BooksController(IBooksService booksService, ILog logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        /// <summary>
        /// Returns List of all books.
        /// </summary>
        /// <returns>All the book details</returns>
        
        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                _logger.Information("Getting all book details");
                var allBooks = _booksService.GetAllBooks();
                return Ok(allBooks);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception occured { ex }");
                return BadRequest("Could not fetch Books detail");
            }
        }

        /// <summary>
        /// Returns a Book matching the given book id.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book</param>
        /// <returns>A matching book</returns>
        
        [HttpGet("GetBookById/{bookId:Guid}")]
        public IActionResult GetBookById(Guid bookId)
        {
            try
            {
                _logger.Information("Getting book details by unique id");
                var book =  _booksService.GetBookById(bookId);
                if (book == null)
                {
                    return NotFound("Invalid book id.");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception occured {ex}");
                return BadRequest("Could not fetch Book by its Id");
            }
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">Book details</param>
        [HttpPost]
        [Route("CreateBook")]
        public IActionResult CreateBook(BookRequest book)
        {
            try
            {
                if (book == null || !ModelState.IsValid)
                {
                    return BadRequest("Please provide valid book information");
                }
                _logger.Information("Creating book");
                _booksService.CreateBook(book);
                return Ok("Book created successfully");
                // return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception occured {ex}");
                return BadRequest("Error while creating a book");
            }

        }

        /// <summary>
        /// Updates existing book details.
        /// </summary>
        /// <param name="book">Book details</param>
        
        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(Book book)
        {
            try
            {
                if (book == null || !ModelState.IsValid)
                {
                    return BadRequest("Please provide valid book information");
                }
                _logger.Information("Updating book");
                var existingBook = _booksService.GetBookById(book.Id);
                if (existingBook == null)
                {
                    return NotFound("Invalid book Id");
                }
                _booksService.UpdateBookAsync(book);
                return Ok("Book details modified successfully");
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception occured {ex}");
                return BadRequest("Error while updating book details");
            }

        }
    }
}