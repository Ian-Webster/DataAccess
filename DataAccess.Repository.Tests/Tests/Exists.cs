using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class Exists: RepositoryTestBase<Book>
{
    [TestCase("29306895-FF91-4D1C-AEC9-18DB5C7B19C2")]
    [TestCase("D946F896-D2BB-42D5-8D10-84495965BA68")]
    [TestCase("14E89AF0-3172-41A5-A061-4D15422BB949")]
    public async Task Should_Return_True_When_BookExists(string bookIdString)
    {
        // arrange
        var bookId = Guid.Parse(bookIdString);

        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.Exists(b => b.BookId == bookId, Token);

        // assert
        Assert.That(result, Is.True);
    }

    [TestCase("D2FD9BD6-6439-4A59-8C28-A6CBE947A55A")]
    [TestCase("6511BF7F-5B22-4A17-9872-26DF886DBADD")]
    [TestCase("6169DFDA-3326-4519-8757-830C3A74A688")]
    public async Task Should_Return_False_When_BookDoesNotExist(string bookIdString)
    {
        // arrange
        var bookId = Guid.Parse(bookIdString);

        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.Exists(b => b.BookId == bookId, Token);

        // assert
        Assert.That(result, Is.False);
    }
}