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
        Assert.IsTrue(result);

        var books = await dbSet.ToListAsync();
        Assert.That(BookTestData.GetBookData().Count, Is.EqualTo(books.Count));

        var updatedBook = await dbSet.FirstOrDefaultAsync(b => b.BookId == bookToUpdate.BookId, Token);
        Assert.IsNotNull(updatedBook);
        StringAssert.AreEqualIgnoringCase(bookToUpdate.Name, updatedBook.Name);
        Assert.IsFalse(books.Any(b => b.BookId != bookToUpdate.BookId && b.Name == bookToUpdate.Name));
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
        Assert.IsFalse(result);

        var books = await dbSet.ToListAsync();
        Assert.That(BookTestData.GetBookData().Count, Is.EqualTo(books.Count));

        var updatedBook = await dbSet.FirstOrDefaultAsync(b => b.Name == bookToUpdate.Name, Token);
        Assert.IsNull(updatedBook);
    }
}