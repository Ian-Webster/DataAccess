using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repository;

public class UnitOfWork<TContext> : IDisposable where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ILogger _logger;
    private bool _disposed;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(TContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger<UnitOfWork<TContext>>();
    }

    public IRepository<T> Repository<T>() where T : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<T>(_context);
        }

        return (IRepository<T>)_repositories[type];
    }

    public async Task<bool> Save(CancellationToken token)
    {
        try
        {
            return await _context.SaveChangesAsync(token) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UnitOfWork failed to save changes");
            return false;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
