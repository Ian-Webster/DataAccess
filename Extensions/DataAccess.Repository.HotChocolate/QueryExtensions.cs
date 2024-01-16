using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.HotChocolate;

/// <summary>
/// Extends IRepository to include GraphQL query functionality supplied by the HotChocolate library
/// </summary>
/// <remarks>
/// see https://chillicream.com/docs/hotchocolate/v13
/// </remarks>
public static class QueryExtensions
{
    /// <summary>
    /// Returns a single TEntity, filtering and projecting using extension methods from https://chillicream.com/docs/hotchocolate/v13/api-reference/extending-filtering
    /// </summary>
    /// <typeparam name="TEntity">entity type for the repository</typeparam>
    /// <param name="repository">IRepository</param>
    /// <param name="context">HotChocolate filter context, this is used by the IQueryable extensions to apply filtering and projection</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<TEntity?> GetQueryItem<TEntity>(this IRepository<TEntity> repository,
        IResolverContext context, CancellationToken token) where TEntity : class
    {
        return await repository.DbSet
            .AsQueryable()
            .AsNoTracking()
            .Filter(context)
            .Project(context)
            .FirstOrDefaultAsync(token);
    }

    /// <summary>
    /// Returns a collection of TEntity, filtered, sorted and projected using extension methods from https://chillicream.com/docs/hotchocolate/v13/api-reference/extending-filtering
    /// </summary>
    /// <typeparam name="TEntity">entity type for the repository</typeparam>
    /// <param name="repository">IRepository</param>
    /// <param name="context">HotChocolate filter context, this is used by the IQueryable extensions to apply filtering, sorting and projection</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<IEnumerable<TEntity>?> GetQueryItems<TEntity>(this IRepository<TEntity> repository, 
        IResolverContext context, CancellationToken token) where TEntity : class
    {
        return await repository.DbSet
            .AsQueryable()
            .AsNoTracking()
            .Filter(context)
            .Project(context)
            .Sort(context)
            .ToListAsync(token);
    }

    /// <summary>
    /// Returns a collection of TEntity, filtered, sorted, paged and projected using extension methods from https://chillicream.com/docs/hotchocolate/v13/api-reference/extending-filtering
    /// </summary>
    /// <remarks>
    /// We cannot return IEnumerable for paged data as we did in <see cref="GetQueryItems{TEntity}"/> because we need additional data
    /// (such as total number of pages, cursor data etc) so instead we return the Connections type provided by HotChocolate
    /// see https://chillicream.com/docs/hotchocolate/v13/fetching-data/pagination/#connections
    /// </remarks>
    /// <typeparam name="TEntity">entity type for the repository</typeparam>
    /// <param name="repository">IRepository</param>
    /// <param name="context">HotChocolate filter context, this is used by the IQueryable extensions to apply filtering, sorting, pagination and projection</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<Connection<TEntity>> GetPagedQueryItems<TEntity>(this IRepository<TEntity> repository,
        IResolverContext context, CancellationToken token) where TEntity : class
    {
        return await repository.DbSet
            .AsNoTracking()
            .AsQueryable()
            .Filter(context)
            .Project(context)
            .Sort(context)
            .ApplyCursorPaginationAsync(context, cancellationToken:token);
    }

    /// <summary>
    /// Returns a collection of TEntity, filtered, sorted, offset paged and projected using extension methods from https://chillicream.com/docs/hotchocolate/v13/api-reference/extending-filtering
    /// </summary>
    /// <remarks>
    /// Variation of cursor paging that uses skip/take instead of cursor data, see https://chillicream.com/docs/hotchocolate/v13/fetching-data/pagination/#offset-pagination
    /// </remarks>
    /// <typeparam name="TEntity">entity type for the repository</typeparam>
    /// <param name="repository">IRepository</param>
    /// <param name="context">HotChocolate filter context, this is used by the IQueryable extensions to apply filtering, sorting, pagination and projection</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static async Task<CollectionSegment<TEntity>> GetOffsetPagedQueryItems<TEntity>(this IRepository<TEntity> repository,
               IResolverContext context, CancellationToken token) where TEntity : class
    {
        var take = context.ArgumentValue<int>("take");
        var skip = context.ArgumentValue<int>("skip");

        return await repository.DbSet
            .AsNoTracking()
            .AsQueryable()
            .Filter(context)
            .Project(context)
            .Sort(context)
            .ApplyOffsetPaginationAsync(skip, take, token);
    }
}