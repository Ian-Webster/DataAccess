using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class Remove: RepositoryTestBase<Book>
{
    [TestCaseSource(nameof(RemoveBookTestCaseData))]
    public async Task Should_Remove_Expected_Item(Guid bookIdToRemove)
    {
        // arrange
        var context = GetContext();
        var dbSet = GetDbSet(context);
        var repo = GetRepository(context);
        
        await InsertData(BookDummyData.BookTestData, context);

        var bookToRemove = await dbSet.FirstAsync(e => e.BookId == bookIdToRemove);

        // act
        var result = await repo.Remove(bookToRemove, Token);

        // assert
        Assert.IsTrue(result);

        var books = await dbSet.ToListAsync();

        Assert.IsFalse(books.Any(b => b.BookId == bookIdToRemove));
        Assert.AreEqual(books.Count, BookDummyData.BookTestData.Count -1);
    }
}