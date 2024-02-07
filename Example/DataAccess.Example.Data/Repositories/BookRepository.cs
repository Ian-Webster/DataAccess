using DataAccess.Example.Data.DatabaseContexts;
using DataAccess.Example.Data.Entities;
using DataAccess.Example.Data.Models;
using DataAccess.Repository;
using DataAccess.Repository.HotChocolate;
using DataAccess.Repository.Models;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;

namespace DataAccess.Example.Data.Repositories;

public class BookRepository: IBookRepository
{
    private readonly IRepository<Book> _bookRepo;

    public BookRepository(UnitOfWork<LibraryDatabaseContext> unitOfWork)
    {
        _bookRepo = unitOfWork.Repository<Book>();
    }

    public async Task<Book?> GetBookById(Guid bookId, CancellationToken token)
    {
        return await _bookRepo.FirstOrDefault(b => b.BookId == bookId, token);
    }

    public async Task<IEnumerable<Book>?> GetAllBooks(CancellationToken token, int? take = null)
    {
        return await _bookRepo.List(p => true, token, take);
    }

    public async Task<PagedResult<Book>> GetPageBooks(PagingRequest request, CancellationToken token)
    {
        return await _bookRepo.Paged(b => true, request, token);
    }

    public async Task<BookNameOnly?> GetBookNameOnlyById(Guid bookId, CancellationToken token)
    {
        return await _bookRepo.FirstOrDefaultProjected(b => b.BookId == bookId,
            b => new BookNameOnly { BookName = b.Name }, token);
    }

    public async Task<IEnumerable<BookNameOnly>?> GetAllBookNamesOnly(CancellationToken token, int? take = null)
    {
        return await _bookRepo.ListProjected(b => true, 
            b => new BookNameOnly { BookName = b.Name }, token, take);
    }

    public async Task<PagedResult<BookNameOnly>> GetPagedBookNamesOnly(PagingRequest request, CancellationToken token)
    {
        return await _bookRepo.PagedProjected(b => true, 
                       b => new BookNameOnly { BookName = b.Name }, request, token);
    }

    public async Task<bool> AddBook(Book bookToAdd, CancellationToken token)
    {
        if (await _bookRepo.Exists(b => b.BookId == bookToAdd.BookId, token))
        {
            return false;
        }

        return await _bookRepo.Add(bookToAdd, token);
    }

    public async Task<bool> UpdateBook(Book bookToUpdate, CancellationToken token)
    {
        var existingBook = await _bookRepo.FirstOrDefault(b => b.BookId == bookToUpdate.BookId, token);
        if (existingBook == null)
        {
            return false;
        }

        existingBook.Name = bookToUpdate.Name;
        return await _bookRepo.Update(existingBook, token);
    }

    public async Task<bool> RemoveBook(Guid bookId, CancellationToken token)
    {
        var bookToRemove = await _bookRepo.FirstOrDefault(b => b.BookId == bookId, token);
        if (bookToRemove == null)
        {
            return false;
        }

        return await _bookRepo.Remove(bookToRemove, token);
    }

    public async Task<Book?> GetBookForGraphQuery(IResolverContext context, CancellationToken token)
    {
        return await _bookRepo.GetQueryItem(context, token);
    }

    public async Task<IEnumerable<Book>?> GetBooksForGraphQuery(IResolverContext context, CancellationToken token)
    {
        return await _bookRepo.GetQueryItems(context, token);
    }

    public async Task<Connection<Book>> GetPagedBooksForGraphQuery(IResolverContext context, CancellationToken token)
    {
        return await _bookRepo.GetPagedQueryItems(context, token);
    }

    public async Task<CollectionSegment<Book>> GetOffsetPagedBooksForGraphQuery(IResolverContext context, CancellationToken token)
    {
        return await _bookRepo.GetOffsetPagedQueryItems(context, token);
    }
}