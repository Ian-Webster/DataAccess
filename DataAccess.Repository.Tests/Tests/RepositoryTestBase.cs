using System.Collections;
using System.Linq.Expressions;
using DataAccess.Repository.Tests.Shared.DatabaseContexts;
using DataAccess.Repository.Tests.Shared.DummyData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

public class RepositoryTestBase<TEntity> where TEntity : class
{
    protected readonly CancellationToken Token;
    protected ILogger Logger;

    [SetUp]
    protected void Setup()
    {
        Logger = NSubstitute.Substitute.For<ILogger>();
    }

    public RepositoryTestBase()
    {
        Token = new CancellationToken();
    }
    
    protected LibraryDatabaseContext GetContext()
    {
        return new LibraryDatabaseContext(new DbContextOptionsBuilder<LibraryDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
    }

    protected DbSet<TEntity> GetDbSet(DbContext context)
    {
        return context.Set<TEntity>();
    }

    protected IRepository<TEntity> GetRepository(ILogger mockLogger, LibraryDatabaseContext? context = null)
    {
        return new UnitOfWork<LibraryDatabaseContext>(context ?? GetContext(), mockLogger).Repository<TEntity>();
    }

    protected async Task InsertData(List<TEntity> entities, DbContext context)
    {
        entities.ForEach(async e =>
        {
            await context.AddAsync(e, Token);
        });
        
        await context.SaveChangesAsync(Token);
    }

    protected static IEnumerable AddBookTestCaseData()
    {
        return BookTestData.GetAddBookTestCaseData();
    }

    protected static IEnumerable UpdateBookTestCaseData()
    {
        return BookTestData.GetUpdateBookTestCaseData();
    }

    protected static IEnumerable UnattachedUpdateBookTestCaseData()
    {
        return BookTestData.GetUnattachedUpdateBookTestCaseData();
    }

    protected static IEnumerable RemoveBookTestCaseData()
    {
        return BookTestData.GetRemoveBookTestCaseData();
    }

    protected static IEnumerable UnAttachedRemoveBookTestCaseData()
    {
        return BookTestData.GetUnAttachedRemoveBookTestCaseData();
    }
    
    protected static IEnumerable FirstOrDefaultBookTestCaseData()
    {
        return BookTestData.GetFirstOrDefaultBookTestCaseData();
    }

    protected static IEnumerable ListBookTestCaseData()
    {
        return BookTestData.GetListBooksTestCaseData();
    }

    public static IEnumerable PagedBookTestCaseData()
    {
        return BookTestData.PagedBookTestCaseData();
    }
}