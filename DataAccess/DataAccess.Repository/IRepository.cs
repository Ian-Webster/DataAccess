using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity: class
    {
        /// <summary>
        /// Gets a single object of type TEntity matching the given entity
        /// </summary>
        /// <param name="predicate">entity expression used to find the object</param>
        /// <param name="token"></param>
        /// <returns>Matching TEntity or null (if no match found)</returns>
        Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken token);

        /// <summary>
        /// Gets a list of objects of type TEntity matching the given entity
        /// </summary>
        /// <param name="predicate">entity expression used to list objects</param>
        /// <param name="token"></param>
        /// <returns>IEnumerable list of TEntity</returns>
        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate, CancellationToken token);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">entity to add</param>
        /// <param name="token"></param>
        /// <returns>true if the add was successful, false if not</returns>
        Task<bool> Add(TEntity entity, CancellationToken token);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="entity">entity to remove</param>
        /// <param name="token"></param>
        /// <returns>true if the remove was successful, false if not</returns>
        Task<bool> Remove(TEntity entity, CancellationToken token);
    }
}
