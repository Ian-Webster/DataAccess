using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class RepositoryFactory
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public RepositoryFactory(DbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<T> GetRepositoryByType<T>() where T : class
    {
        var type = typeof(T);
        if (_repositories.ContainsKey(type))
        {
            return (IRepository<T>)_repositories[type];
        }

        var repository = new Repository<T>(_context);

        _repositories.Add(type, repository);
        
        return repository;
    }
}