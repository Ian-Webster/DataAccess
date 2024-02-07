using DataAccess.Example.Data.Entities;
using DataAccess.Example.Data.Models;
using DataAccess.Example.Data.Repositories;
using DataAccess.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAccess.Example.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BookController(IBookRepository bookRepository, IServiceScopeFactory serviceScopeFactory)
        {
            _bookRepository = bookRepository;
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Get's all books in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            return Ok(await _bookRepository.GetAllBooks(Token));
        }

        /// <summary>
        /// Get's X books where X is the count parameter
        /// </summary>
        /// <param name="count">number of books to get</param>
        /// <returns></returns>
        [HttpGet("limit/{count:int}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllLimit([FromRoute]int count)
        {
            return Ok(await _bookRepository.GetAllBooks(Token, count));
        }

        /// <summary>
        /// Gets a book by it's id
        /// </summary>
        /// <param name="bookId">id of the book to get</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{bookId:guid}")]
        public async Task<ActionResult<Book>> GetBookById([FromRoute]Guid bookId)
        {
            var book = await _bookRepository.GetBookById(bookId, Token);

            return book != null ? Ok(book) : NotFound($"Book with id {bookId} not found");
        }

        /// <summary>
        /// Get's all books paged
        /// </summary>
        /// <param name="request">paging data</param>
        /// <returns></returns>
        [HttpPost("paged")]
        public async Task<ActionResult<PagedResult<Book>>> GetPagedBooks([FromBody] PagingRequest request)
        {
            return Ok(await _bookRepository.GetPageBooks(request, Token));
        }

        /// <summary>
        /// Get's a book projected into a BookNameOnly object by id
        /// </summary>
        /// <param name="bookId">id of the book to get</param>
        /// <returns></returns>
        [HttpGet("nameOnly/{bookId:guid}")]
        public async Task<ActionResult<BookNameOnly>> GetBookNameOnlyById([FromRoute] Guid bookId)
        {
            var book = await _bookRepository.GetBookNameOnlyById(bookId, Token);

            return book != null ? Ok(book) : NotFound($"Book with id {bookId} not found");
        }

        /// <summary>
        /// Get's all books projected into a BookNameOnly object
        /// </summary>
        /// <param name="count">count of books to get</param>
        /// <returns></returns>
        [HttpGet("nameOnly/{count:int}")]
        public async Task<ActionResult<IEnumerable<BookNameOnly>>> GetAllBookNamesOnly([FromRoute]int count)
        {
            return Ok(await _bookRepository.GetAllBookNamesOnly(Token));
        }

        /// <summary>
        /// Get's all books projected into a BookNameOnly object paged
        /// </summary>
        /// <param name="request">paging request data</param>
        /// <returns></returns>
        [HttpPost("nameOnly/paged")]
        public async Task<ActionResult<PagedResult<BookNameOnly>>> GetPagedBookNamesOnly([FromBody] PagingRequest request)
        {
            return Ok(await _bookRepository.GetPagedBookNamesOnly(request, Token));
        }

        /// <summary>
        /// Test for threading issues with the repository code
        /// </summary>
        /// <returns></returns>
        [HttpGet("theadTest")]
        public async Task<ActionResult> ThreadTest()
        {
            var task1 = Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                await repo.GetAllBooks(Token);
            });

            var task2 = Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                await repo.GetAllBooks(Token);
            });

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
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (await _bookRepository.AddBook(book, Token))
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
        public async Task<IActionResult> UpdateBook([FromBody]Book book)
        {
            if (await _bookRepository.UpdateBook(book, Token))
            {
                return Ok();
            }

            return UnprocessableEntity($"Failed to update book with id {book.BookId}");
        }

        
        [HttpDelete("{bookId:guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute]Guid bookId)
        {
            if (await _bookRepository.RemoveBook(bookId, Token))
            {
                return Ok();
            }

            return UnprocessableEntity($"Failed to delete book with id {bookId}");
        }
    }
}
