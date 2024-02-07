using System.Linq.Expressions;
using DataAccess.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

// TODO: figure out how to extend this so the dbContext can be shared but not exposed outside of this class
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _context;

    public Repository(DbContext context)
    { 
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public DbSet<TEntity> DbSet => _dbSet;

    public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
    {
        return await _dbSet.AnyAsync(predicate, token);
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, token);
    }

    public async Task<IEnumerable<TEntity>?> List(Expression<Func<TEntity, bool>> predicate, CancellationToken token, int? take = null)
    {
        var query = _dbSet.Where(predicate);

        if (query == null) return null;

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync(cancellationToken: token);
    }

    public async Task<PagedResult<TEntity>> Paged(Expression<Func<TEntity, bool>> predicate, PagingRequest pagingRequest, CancellationToken token)
    {
        var query = _dbSet.Where(predicate);

        if (query == null || ! await query.AnyAsync(token))
        {
            return new PagedResult<TEntity>
            {
                Data = null,
                PageIndex = 0,
                PageSize = pagingRequest.PageSize,
                TotalCount = 0
            };
        }

        var count = await query.CountAsync(token);

        var data = await query.Skip(pagingRequest.PageSize * pagingRequest.PageIndex)
            .Take(pagingRequest.PageSize).ToListAsync(token);

        return new PagedResult<TEntity>
        {
            Data = data,
            PageIndex = pagingRequest.PageIndex,
            PageSize = pagingRequest.PageSize,
            TotalCount = count
        };
    }

    public async Task<bool> Add(TEntity entity, CancellationToken token)
    {
        await _context.AddAsync(entity, token);

        return await SaveChanges(token);
    }

    public async Task<bool> Update(TEntity entity, CancellationToken token)
    {
        if (!EntityIsAttached(entity))
        {
            // Maybe this should be an exception? someone is misusing the method
            return false;
        }

        _dbSet.Update(entity);

        return await SaveChanges(token);
    }

    public async Task<bool> Remove(TEntity entity, CancellationToken token)
    {
        if (!EntityIsAttached(entity))
        {
            // Maybe this should be an exception? someone is misusing the method
            return false;
        }

        _context.Remove(entity);

        return await SaveChanges(token);
    }

    private async Task<bool> SaveChanges(CancellationToken token)
    {
        return await _context.SaveChangesAsync(token) > 0;
    }

    private bool EntityIsAttached(TEntity entity)
    {
        return _dbSet.Local.Any(l => l == entity);
    }

    public async Task<TProjected?> FirstOrDefaultProjected<TProjected>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection, CancellationToken token)
    {
        return await _dbSet.Where(predicate).Select(projection).FirstOrDefaultAsync(token);
    }

    public async Task<IEnumerable<TProjected>?> ListProjected<TProjected>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection, CancellationToken token, int? take = null)
    {
        var query = _dbSet.Where(predicate).Select(projection);

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync(token);
    }

    public async Task<PagedResult<TProjected>> PagedProjected<TProjected>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection, PagingRequest pagingRequest, CancellationToken token) where TProjected : class
    {
        var query = _dbSet.Where(predicate);

        if (query == null || !await query.AnyAsync(token))
        {
            return new PagedResult<TProjected>
            {
                Data = null,
                PageIndex = 0,
                PageSize = pagingRequest.PageSize,
                TotalCount = 0
            };
        }

        var count = await query.CountAsync(token);

        var data = await query.Skip(pagingRequest.PageSize * pagingRequest.PageIndex)
            .Take(pagingRequest.PageSize).Select(projection).ToListAsync(token);

        return new PagedResult<TProjected>
        {
            Data = data,
            PageIndex = pagingRequest.PageIndex,
            PageSize = pagingRequest.PageSize,
            TotalCount = count
        };
    }
}
