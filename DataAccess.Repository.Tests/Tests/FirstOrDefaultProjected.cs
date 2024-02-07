using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using DataAccess.Repository.Tests.Shared.Projections;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class FirstOrDefaultProjected: RepositoryTestBase<Book>
{
    [Test]
    public async Task Should_Return_Null_When_NoDataExists()
    {
        // arrange
        var repo = GetRepository(LoggerFactory, GetContext());

        // act
        var result = await repo.FirstOrDefaultProjected(p
            => p.Name != string.Empty,
            BookProjections.BookToBookProjected(), Token);

        // assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Should_Return_Null_When_DataNotFound()
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.FirstOrDefaultProjected(
            p => p.BookId == Guid.Empty,
            BookProjections.BookToBookProjected(),
            Token);

        // assert
        Assert.That(result, Is.Null);
    }

    [TestCaseSource(nameof(FirstOrDefaultBookTestCaseData))]
    public async Task Should_Get_Expected_Item_When_MatchingItemFound(Guid bookId)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);
        
        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.FirstOrDefaultProjected(p => p.BookId == bookId,
            BookProjections.BookToBookProjected(), Token);

        // assert
        Assert.That(result, Is.Not.Null);

        var expectedBook = BookTestData.GetBookData()
            .Where(b => b.BookId == bookId)
            .Select(b => new ProjectedBook{BookName = b.Name}).First();
         
        Assert.That(JsonConvert.SerializeObject(expectedBook), Is.EqualTo(JsonConvert.SerializeObject(result)));
    }
}