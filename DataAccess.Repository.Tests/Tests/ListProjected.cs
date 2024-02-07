using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using DataAccess.Repository.Tests.Shared.Projections;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class ListProjected : RepositoryTestBase<Book>
{
    [Test]
    public async Task Should_Return_EmptyList_When_NoDataExists()
    {
        // arrange
        var repo = GetRepository(LoggerFactory, GetContext());

        // act
        var result = await repo.ListProjected(p => 
            p.Name.Contains("Book"),
            BookProjections.BookToBookProjected(), 
            Token);

        // assert
        var enumerable = result.ToList();
        Assert.That(enumerable, Is.Not.Null);
        Assert.That(enumerable, Is.Empty);
    }

    [Test]
    public async Task Should_Return_EmptyList_When_NoMatchingDataFound()
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.ListProjected(p =>
                p.Name.Contains("wobble"),
            BookProjections.BookToBookProjected(),
            Token);

        // assert
        var enumerable = result.ToList();
        Assert.That(enumerable, Is.Not.Null);
        Assert.That(enumerable, Is.Empty);
    }

    [TestCaseSource(nameof(ListBookTestCaseData))]
    public async Task Should_Return_Expected_Data_WhenMatchingDataFound(string searchString)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.ListProjected(p =>
                p.Name.Contains(searchString),
            BookProjections.BookToBookProjected(),
            Token);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);

        var expectedBooks = BookTestData.GetBookData()
            .Where(b => b.Name.Contains(searchString))
            .Select(b => new ProjectedBook { BookName = b.Name})
            .ToList();
        Assert.That(expectedBooks.Count, Is.EqualTo(result.Count()));
        StringAssert.AreEqualIgnoringCase(JsonConvert.SerializeObject(expectedBooks),
            JsonConvert.SerializeObject(result));
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task Should_Return_LimitedData_When_TakeParameterIsUsed(int take)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        var testData = BookTestData.GetBookData();
        await InsertData(testData, context);

        // act
        var result = await repo.ListProjected(p =>
            p.Name.Contains("Book"), 
            BookProjections.BookToBookProjected(),
            Token, 
            take);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(take));
    }

}