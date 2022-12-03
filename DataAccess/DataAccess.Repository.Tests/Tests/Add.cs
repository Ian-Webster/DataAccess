using DataAccess.Repository.Tests.Shared.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        var repo = GetRepository(context);

        // act
        var result = await repo.Add(data, Token);

        // assert
        Assert.IsTrue(result);

        var addedBook = await repo.FirstOrDefault(p => p.BookId == data.BookId, Token);

        Assert.IsNotNull(addedBook);
        StringAssert.AreEqualIgnoringCase(JsonConvert.SerializeObject(data), JsonConvert.SerializeObject(addedBook));
    }
}