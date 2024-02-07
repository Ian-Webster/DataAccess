using DataAccess.Example.Data.Entities;
using DataAccess.Example.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAccess.Example.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Get's all books in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll(CancellationToken token)
        {
            return Ok(await _bookRepository.GetAllBooks(token));
        }

        /// <summary>
        /// Gets a book by it's id
        /// </summary>
        /// <param name="bookId">id of the book to get</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{bookId:guid}")]
        public async Task<ActionResult<Book>> GetBookById([FromRoute]Guid bookId, CancellationToken token)
        {
            var book = await _bookRepository.GetBookById(bookId, token);

            return book != null ? Ok(book) : NotFound($"Book with id {bookId} not found");
        }

        [HttpGet("theadTest")]
        public async Task<ActionResult> ThreadTest(CancellationToken token)
        {
            var task1 = Task.Run(() => _bookRepository.GetAllBooks(token));
            var task2 = Task.Run(() => _bookRepository.GetAllBooks(token));

            await Task.WhenAll(task1, task2);

            return Ok();
        }

        /// <summary>
        /// Adds a new book to the database
        /// </summary>
        /// <param name="book">book to add</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book, CancellationToken token)
        {
            if (await _bookRepository.AddBook(book, token))
            {
                return Ok();
            }

            return UnprocessableEntity("Failed to add book");
        }

        /// <summary>
        /// Update the given book
        /// </summary>
        /// <param name="book">book to update</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody]Book book, CancellationToken token)
        {
            if (await _bookRepository.UpdateBook(book, token))
            {
                return Ok();
            }

            return UnprocessableEntity($"Failed to update book with id {book.BookId}");
        }

        
        [HttpDelete("{bookId:guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute]Guid bookId, CancellationToken token)
        {
            if (await _bookRepository.RemoveBook(bookId, token))
            {
                return Ok();
            }

            return UnprocessableEntity($"Failed to delete book with id {bookId}");
        }
    }
}
