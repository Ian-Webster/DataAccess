using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
    {
        return await _dbSet.AnyAsync(predicate, token);
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, token);
    }

    public async Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken: token);
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

}
