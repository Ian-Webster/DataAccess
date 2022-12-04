using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Tests.Shared.DatabaseContexts;

public class LibraryDatabaseContext: DbContext
{
    public LibraryDatabaseContext(DbContextOptions options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}