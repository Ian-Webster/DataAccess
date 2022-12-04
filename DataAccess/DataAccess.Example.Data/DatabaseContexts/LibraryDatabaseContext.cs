using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Example.Data.DatabaseContexts;

public class LibraryDatabaseContext: DbContext
{
    public LibraryDatabaseContext(DbContextOptions options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(LibraryDatabaseContext)));
    }
}