using System.Linq.Expressions;
using DataAccess.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity: class
    {

        public DbSet<TEntity> DbSet { get;  }

        #region entity methods

        /// <summary>
        /// Checks if a object matching the given predicate exists
        /// </summary>
        /// <param name="predicate">predicate used to check existence</param>
        /// <param name="token"></param>
        /// <returns>True if a matching object exists, false if not</returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate, CancellationToken token);

        /// <summary>
        /// Gets a single object of type TEntity matching the given entity
        /// </summary>
        /// <param name="predicate">The predicate used to find the object</param>
        /// <param name="token"></param>
        /// <returns>Matching TEntity or null (if no match found)</returns>
        Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken token);

        /// <summary>
        /// Gets a list of objects of type TEntity matching the given entity
        /// </summary>
        /// <param name="predicate">The predicate used to filter results by</param>
        /// <param name="token"></param>
        /// <param name="take">number of objects to take, optional null if not provided</param>
        /// <returns>IEnumerable list of TEntity</returns>
        Task<IEnumerable<TEntity>?> List(Expression<Func<TEntity, bool>> predicate, CancellationToken token, int? take = null);

        /// <summary>
        /// Gets a paged list of TEntity matching the given predicate
        /// </summary>
        /// <param name="predicate">The predicate to filter results by</param>
        /// <param name="pagingRequest">The paging data for this request</param>
        /// <param name="token">cancallation token</param>
        /// <returns></returns>
        Task<PagedResult<TEntity>> Paged(Expression<Func<TEntity, bool>> predicate, 
            PagingRequest pagingRequest, CancellationToken token);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">entity to add</param>
        /// <param name="token"></param>
        /// <returns>true if the add was successful, false if not</returns>
        Task<bool> Add(TEntity entity, CancellationToken token);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">entity to update</param>
        /// <param name="token"></param>
        /// <returns>true if the update was successful, false if not</returns>
        Task<bool> Update(TEntity entity, CancellationToken token);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="entity">entity to remove</param>
        /// <param name="token"></param>
        /// <returns>true if the remove was successful, false if not</returns>
        Task<bool> Remove(TEntity entity, CancellationToken token);

        #endregion

        #region protected methods

        /// <summary>
        /// Gets a single object of type TProjected matching the given predicate
        /// </summary>
        /// <typeparam name="TProjected">The type you want to return</typeparam>
        /// <param name="predicate">Used to find a single matching entity</param>
        /// <param name="projection">An expression to convert TEntity to TProjected</param>
        /// <param name="token">Cancellation token</param>
        /// <returns></returns>
        Task<TProjected?> FirstOrDefaultProjected<TProjected>(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, TProjected>> projection, CancellationToken token);

        /// <summary>
        /// Gets a list of TProjected matching the given predicate
        /// </summary>
        /// <typeparam name="TProjected">The type you want to return</typeparam>
        /// <param name="predicate">The predicate to filter results by</param>
        /// <param name="projection">An expression to convert TEntity to TProjected</param>
        /// <param name="token">Cancellation token</param>
        /// <param name="take">number of items to return</param>
        /// <returns></returns>
        Task<IEnumerable<TProjected>?> ListProjected<TProjected>(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, TProjected>> projection, CancellationToken token, int? take = null);

        /// <summary>
        /// Gets a paged list of TProjected matching the given predicate
        /// </summary>
        /// <param name="predicate">The predicate to filter results by</param>
        /// <param name="projection">An expression to convert TEntity to TProjected</param>
        /// <param name="pagingRequest">The paging data for this request</param>
        /// <param name="token">cancallation token</param>
        /// <returns></returns>
        Task<PagedResult<TProjected>> PagedProjected<TProjected>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProjected>> projection, 
            PagingRequest pagingRequest, CancellationToken token) where TProjected : class;

        #endregion
    }
}
