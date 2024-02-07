using DataAccess.Repository.Tests.Shared.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Tests;

[TestFixture]
public class Add: RepositoryTestBase<Book>
{
    [TestCaseSource(nameof(AddBookTestCaseData))]
    public async Task Should_Add_Expected_Data(Book data)
    {
        // arrange
        var context = GetContext();
        var repo = GetRepository(LoggerFactory, context);

        // act
        var result = await repo.Add(data, Token);

        // assert
        Assert.That(result, Is.True);

        var addedBook = await repo.FirstOrDefault(p => p.BookId == data.BookId, Token);

        Assert.That(JsonConvert.SerializeObject(data), Is.EqualTo(JsonConvert.SerializeObject(addedBook)));
    }
}