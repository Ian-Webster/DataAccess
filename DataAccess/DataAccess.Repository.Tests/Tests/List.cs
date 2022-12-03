using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class List: RepositoryTestBase<Book>
{
    [Test]
    public async Task Should_Return_EmptyList_When_NoDataExists()
    {
        // arrange
        var repo = GetRepository();

        // act
        var result = await repo.List(p => p.Name.Contains("Book"), Token);

        // assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public async Task Should_Return_EmptyList_When_NoMatchingDataFound()
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(context);

        await InsertData(BookDummyData.BookTestData, context);

        // act
        var result = await repo.List(p => p.Name.Contains("wobble"), Token);

        // assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [TestCaseSource(nameof(ListBookTestCaseData))]
    public async Task Should_Return_Expected_Data_WhenMatchingDataFound(string searchString)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(context);

        await InsertData(BookDummyData.BookTestData, context);

        // act
        var result = await repo.List(p => p.Name.Contains(searchString), Token);

        // assert
        Assert.IsNotEmpty(result);

        var expectedBooks = BookDummyData.BookTestData.Where(b => b.Name.Contains(searchString)).ToList();
        Assert.AreEqual(expectedBooks.Count, result.Count());
        expectedBooks.ForEach(b =>
        {
            Assert.IsTrue(result.Any(r => r.BookId == b.BookId));
        });

}
}