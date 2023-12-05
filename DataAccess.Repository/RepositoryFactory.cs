using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class RepositoryFactory<TContext>(TContext context)
    where TContext : DbContext
{
    private readonly DbContext _context = context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IRepository<T> GetRepositoryByType<T>() where T : class
    {
        var type = typeof(T);
        if (_repositories.TryGetValue(type, out var repo))
        {
            return (IRepository<T>)repo;
        }

        var repository = new Repository<T>(_context);

        _repositories.Add(type, repository);
        
        return repository;
    }
}