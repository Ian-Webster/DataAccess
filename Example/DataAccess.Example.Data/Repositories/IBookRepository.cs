using DataAccess.Example.Data.Entities;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;

namespace DataAccess.Example.Data.Repositories;

public interface IBookRepository
{
    Task<Book?> GetBookById(Guid bookId, CancellationToken token);

    Task<IEnumerable<Book>?> GetAllBooks(CancellationToken token);

    Task<bool> AddBook(Book bookToAdd, CancellationToken token);

    Task<bool> UpdateBook(Book bookToUpdate, CancellationToken token);

    Task<bool> RemoveBook(Guid bookId, CancellationToken token);

    Task<Book?> GetBookForGraphQuery(IResolverContext context, CancellationToken token);

    Task<IEnumerable<Book>?> GetBooksForGraphQuery(IResolverContext context, CancellationToken token);

    Task<Connection<Book>> GetPagedBooksForGraphQuery(IResolverContext context, CancellationToken token);
}