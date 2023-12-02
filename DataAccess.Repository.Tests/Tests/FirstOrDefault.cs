using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class FirstOrDefault: RepositoryTestBase<Book>
{
    [Test]
    public async Task Should_Return_Null_When_NoDataExists()
    {
        // arrange
        var repo = GetRepository();

        // act
        var result = await repo.FirstOrDefault(p => p.Name != String.Empty, Token);

        // assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Should_Return_Null_When_DataNotFound()
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.FirstOrDefault(p => p.BookId == Guid.Empty, Token);

        // assert
        Assert.That(result, Is.Null);
    }

    [TestCaseSource(nameof(FirstOrDefaultBookTestCaseData))]
    public async Task Should_Get_Expected_Item_When_MatchingItemFound(Guid bookId)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(context);
        
        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.FirstOrDefault(p => p.BookId == bookId, Token);

        // assert
        Assert.That(result, Is.Not.Null);

        var expectedBook = BookTestData.GetBookData().First(b => b.BookId == bookId);

        Assert.That(JsonConvert.SerializeObject(expectedBook), Is.EqualTo(JsonConvert.SerializeObject(result)));
    }
}