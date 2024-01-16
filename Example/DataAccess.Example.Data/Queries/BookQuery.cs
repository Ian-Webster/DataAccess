using DataAccess.Example.Data.Entities;
using DataAccess.Example.Data.Repositories;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;

namespace DataAccess.Example.Data.Queries;

[ExtendObjectType("Query")]
public class BookQuery
{
    [UseProjection]
    [UseFiltering]
    public async Task<Book?> GetBook([Service] IBookRepository repository, IResolverContext context, CancellationToken token)
    {
        return await repository.GetBookForGraphQuery(context, token);
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Book>?> GetBooks([Service] IBookRepository repository, IResolverContext context, CancellationToken token)
    {
        return await repository.GetBooksForGraphQuery(context, token);
    }

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<Connection<Book>> GetPagedBooks([Service] IBookRepository repository, IResolverContext context, CancellationToken token)
    {
        return await repository.GetPagedBooksForGraphQuery(context, token);
    }

    public async Task<CollectionSegment<Book>> GetOffsetPagedBooks([Service] IBookRepository repository, IResolverContext context, CancellationToken token)
    {
        return await repository.GetOffsetPagedBooksForGraphQuery(context, token);
    }
}