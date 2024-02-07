using DataAccess.Example.Data.Entities;
using DataAccess.Example.Data.Models;
using DataAccess.Repository.Models;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;

namespace DataAccess.Example.Data.Repositories;

public interface IBookRepository
{
    Task<Book?> GetBookById(Guid bookId, CancellationToken token);

    Task<IEnumerable<Book>?> GetAllBooks(CancellationToken token, int? take = null);

    Task<PagedResult<Book>> GetPageBooks(PagingRequest request, CancellationToken token);

    Task<BookNameOnly?> GetBookNameOnlyById(Guid bookId, CancellationToken token);

    Task<IEnumerable<BookNameOnly>?> GetAllBookNamesOnly(CancellationToken token, int? take = null);

    Task<PagedResult<BookNameOnly>> GetPagedBookNamesOnly(PagingRequest request, CancellationToken token);

    Task<bool> AddBook(Book bookToAdd, CancellationToken token);

    Task<bool> UpdateBook(Book bookToUpdate, CancellationToken token);

    Task<bool> RemoveBook(Guid bookId, CancellationToken token);

    Task<Book?> GetBookForGraphQuery(IResolverContext context, CancellationToken token);

    Task<IEnumerable<Book>?> GetBooksForGraphQuery(IResolverContext context, CancellationToken token);

    Task<Connection<Book>> GetPagedBooksForGraphQuery(IResolverContext context, CancellationToken token);

    Task<CollectionSegment<Book>> GetOffsetPagedBooksForGraphQuery(IResolverContext context, CancellationToken token);
}