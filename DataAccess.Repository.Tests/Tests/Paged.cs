using DataAccess.Repository.Models;
using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class Paged : RepositoryTestBase<Book>
{
    [Test]
    public async Task Should_Return_Null_When_NoDataExists()
    {
        // arrange
        var repo = GetRepository(LoggerFactory, GetContext());
        var pagingRequest = new PagingRequest { PageIndex = 0, PageSize = 10 };

        // act
        var result = await repo.Paged(p => p.Name.Contains("Book"), pagingRequest, Token);

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
        var repo = GetRepository(LoggerFactory, context);
        var pagingRequest = new PagingRequest { PageIndex = 0, PageSize = 10 };

        await InsertData(BookTestData.GetBookData(), context);

        // act
        var result = await repo.Paged(p => p.Name.Contains("wobble"), pagingRequest, Token);

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
        var repo = GetRepository(LoggerFactory, context);
        

        await InsertData(BookTestData.GetPagedBookData(), context);

        // act
        var result = await repo.Paged(p => p.Name.Contains(searchString), pagingRequest, Token);

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data, Is.Not.Empty);
        Assert.That(result.PageIndex, Is.EqualTo(pagingRequest.PageIndex));
        Assert.That(result.PageSize, Is.EqualTo(pagingRequest.PageSize));
        Assert.That(result.TotalCount, Is.EqualTo(expectedTotalCount));

        var data = result.Data.ToList();
        
        Assert.That(data.Count, Is.EqualTo(result.Data.Count()));
        StringAssert.AreEqualIgnoringCase(JsonConvert.SerializeObject(expectedData), JsonConvert.SerializeObject(data));
    }
}
