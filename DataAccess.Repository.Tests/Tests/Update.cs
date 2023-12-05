using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class Update: RepositoryTestBase<Book>
{
    [TestCaseSource(nameof(UpdateBookTestCaseData))]
    public async Task Should_Update_ExpectedBook_When_BookAttached(Book bookToUpdate)
    {
        // arrange
        var context = GetContext();
        var dbSet = GetDbSet(context);
        var repo = GetRepository(context);

        await InsertData(BookTestData.GetBookData(), context);

        var book = await dbSet.FirstAsync(e => e.BookId == bookToUpdate.BookId);
        book.Name = bookToUpdate.Name;

        // act
        var result = await repo.Update(book, Token);

        // assert
        Assert.That(result, Is.True);

        var books = await dbSet.ToListAsync();
        Assert.That(BookTestData.GetBookData().Count, Is.EqualTo(books.Count));

        var updatedBook = await dbSet.FirstOrDefaultAsync(b => b.BookId == bookToUpdate.BookId, Token);
        Assert.That(updatedBook, Is.Not.Null);
        Assert.That(bookToUpdate.Name, Is.EqualTo(updatedBook?.Name));
        Assert.That(books.Any(b => b.BookId != bookToUpdate.BookId && b.Name == bookToUpdate.Name), Is.False);
    }

    [TestCaseSource(nameof(UnattachedUpdateBookTestCaseData))]
    public async Task Should_NotUpdate_Books_When_BookNotAttached(Book bookToUpdate)
    {
        // arrange
        var context = GetContext();
        var dbSet = GetDbSet(context);
        var repo = GetRepository(context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.Update(bookToUpdate, Token);

        // assert
        Assert.That(result, Is.False);

        var books = await dbSet.ToListAsync();
        Assert.That(BookTestData.GetBookData().Count, Is.EqualTo(books.Count));

        var updatedBook = await dbSet.FirstOrDefaultAsync(b => b.Name == bookToUpdate.Name, Token);
        Assert.That(updatedBook, Is.Null);
    }
}