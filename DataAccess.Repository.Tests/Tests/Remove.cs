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
        var repo = GetRepository(context);
        
        await InsertData(BookTestData.GetBookData(), context);

        var bookToRemove = await dbSet.FirstAsync(e => e.BookId == bookIdToRemove);

        // act
        var result = await repo.Remove(bookToRemove, Token);

        // assert
        Assert.IsTrue(result);

        var books = await dbSet.ToListAsync();

        Assert.IsFalse(books.Any(b => b.BookId == bookIdToRemove));
        Assert.That(BookTestData.GetBookData().Count -1, Is.EqualTo(books.Count));
    }

    [TestCaseSource(nameof(UnAttachedRemoveBookTestCaseData))]
    public async Task Should_Return_False_When_TryingToRemove_UnattachedItem(Book bookToRemove)
    {
        // arrange
        var context = GetContext();
        var dbSet = GetDbSet(context);
        var repo = GetRepository(context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.Remove(bookToRemove, Token);

        // assert
        Assert.IsFalse(result);

        var books = await dbSet.ToListAsync();

        Assert.IsTrue(books.Any(b => b.BookId == bookToRemove.BookId));
        Assert.That(BookTestData.GetBookData().Count, Is.EqualTo(books.Count));
    }
}