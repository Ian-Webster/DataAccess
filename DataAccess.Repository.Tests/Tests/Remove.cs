using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class Remove: RepositoryTestBase<Book>
{
    [TestCaseSource(nameof(RemoveBookTestCaseData))]
    public async Task Should_Remove_Expected_AttachedItem(Guid bookIdToRemove)
    {
        // arrange
        var context = GetContext();
        var dbSet = GetDbSet(context);
        var repo = GetRepository(LoggerFactory, context);
        
        await InsertData(BookTestData.GetBookData(), context);

        var bookToRemove = await dbSet.FirstAsync(e => e.BookId == bookIdToRemove);

        // act
        var result = await repo.Remove(bookToRemove, Token);

        // assert
        Assert.That(result, Is.True);

        var books = await dbSet.ToListAsync();

        Assert.That(books.Any(b => b.BookId == bookIdToRemove), Is.False);
        Assert.That(BookTestData.GetBookData().Count -1, Is.EqualTo(books.Count));
    }

    [TestCaseSource(nameof(UnAttachedRemoveBookTestCaseData))]
    public async Task Should_Return_False_When_TryingToRemove_UnattachedItem(Book bookToRemove)
    {
        // arrange
        var context = GetContext();
        var dbSet = GetDbSet(context);
        var repo = GetRepository(LoggerFactory, context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.Remove(bookToRemove, Token);

        // assert
        Assert.That(result, Is.False);

        var books = await dbSet.ToListAsync();

        Assert.That(books.Any(b => b.BookId == bookToRemove.BookId), Is.True);
        Assert.That(BookTestData.GetBookData().Count, Is.EqualTo(books.Count));
    }
}