using DataAccess.Repository.Models;
using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using DataAccess.Repository.Tests.Shared.Projections;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class PageProjected: RepositoryTestBase<Book>
{
    [Test]
    public async Task Should_Return_Null_When_NoDataExists()
    {
        // arrange
        var repo = GetRepository(Logger, GetContext());
        var pagingRequest = new PagingRequest { PageIndex = 0, PageSize = 10 };

        // act
        var result = await repo.PagedProjected(p => p.Name.Contains("Book"), 
            BookProjections.BookToBookProjected() ,pagingRequest, Token);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data, Is.Null);
        Assert.That(result.PageIndex, Is.EqualTo(0));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalCount, Is.EqualTo(0));
    }

    [Test]
    public async Task Should_Return_Null_When_NoMatchingDataFound()
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(Logger, context);
        var pagingRequest = new PagingRequest { PageIndex = 0, PageSize = 10 };

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.PagedProjected(p => p.Name.Contains("wobble"), 
            BookProjections.BookToBookProjected(), pagingRequest, Token);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data, Is.Null);
        Assert.That(result.PageIndex, Is.EqualTo(0));
        Assert.That(result.PageSize, Is.EqualTo(10));
        Assert.That(result.TotalCount, Is.EqualTo(0));
    }

    [TestCaseSource(nameof(PagedBookTestCaseData))]
    public async Task Should_Return_Expected_Data_WhenMatchingDataFound(string searchString, PagingRequest pagingRequest, List<Book> expectedData, int expectedTotalCount)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(Logger, context);


        await InsertData(BookTestData.GetPagedBookData(), context);

        // act
        var result = await repo.PagedProjected(p => p.Name.Contains(searchString), 
            BookProjections.BookToBookProjected(), pagingRequest, Token);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data, Is.Not.Empty);
        Assert.That(result.PageIndex, Is.EqualTo(pagingRequest.PageIndex));
        Assert.That(result.PageSize, Is.EqualTo(pagingRequest.PageSize));
        Assert.That(result.TotalCount, Is.EqualTo(expectedTotalCount));

        var data = result.Data.ToList();

        var projectedData = expectedData.Select(d => new ProjectedBook { BookName = d.Name });

        Assert.That(data.Count, Is.EqualTo(result.Data.Count()));
        StringAssert.AreEqualIgnoringCase(JsonConvert.SerializeObject(projectedData), JsonConvert.SerializeObject(data));
    }
}