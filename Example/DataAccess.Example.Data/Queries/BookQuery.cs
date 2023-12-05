using DataAccess.Example.Data.Entities;
using DataAccess.Example.Data.Repositories;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Resolvers;

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
}