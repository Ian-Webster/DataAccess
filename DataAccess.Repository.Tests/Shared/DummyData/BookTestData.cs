using System.Collections;
using DataAccess.Repository.Models;
using DataAccess.Repository.Tests.Shared.Entities;
using DataAccess.Repository.Tests.Tests;
using NUnit.Framework;

namespace DataAccess.Repository.Tests.Shared.DummyData;

public static class BookTestData
{
    public static List<Book> GetBookData()
    {
        return new List<Book>
        {
            new Book
            {
                BookId = Guid.Parse("29306895-FF91-4D1C-AEC9-18DB5C7B19C2"),
                Name = "Book A"
            },
            new Book
            {
                BookId = Guid.Parse("D946F896-D2BB-42D5-8D10-84495965BA68"),
                Name = "Book B"
            },
            new Book
            {
                BookId = Guid.Parse("14E89AF0-3172-41A5-A061-4D15422BB949"),
                Name = "Book C"
            },
            new Book
            {
                BookId = Guid.Parse("83A1FB0A-0DF9-4C45-A92C-59F0A7CDEFFE"),
                Name = "Book D"
            },
            new Book
            {
                BookId = Guid.Parse("B87987F0-B455-4376-856A-C7863BEED777"),
                Name = "Novel i"
            },
            new Book
            {
                BookId = Guid.Parse("239C9AB0-F647-4F89-9019-64BC932D03A4"),
                Name = "Novel ii"
            },
            new Book
            {
                BookId = Guid.Parse("FDF3A8D2-900D-4E68-9DFB-00FA3A157620"),
                Name = "Novel iii"
            }
        };
    }

    public static List<Book> GetPagedBookData()
    {
        return new List<Book>
        {
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Book 1" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Book 2" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Book 3" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Book 4" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Book 5" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Book 6" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Book 7" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Book 8" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Book 9" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Book 10" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Book 11" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "Book 12" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "Book 13" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "Book 14" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000015"), Name = "Book 15" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000016"), Name = "Book 16" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000017"), Name = "Book 17" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000018"), Name = "Book 18" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000019"), Name = "Book 19" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000020"), Name = "Book 20" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000021"), Name = "Book 21" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000022"), Name = "Book 22" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000023"), Name = "Book 23" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000024"), Name = "Book 24" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000025"), Name = "Book 25" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000026"), Name = "Book 26" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000027"), Name = "Book 27" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000028"), Name = "Book 28" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000029"), Name = "Book 29" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000030"), Name = "Book 30" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000031"), Name = "Book 31" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000032"), Name = "Book 32" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000033"), Name = "Book 33" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000034"), Name = "Book 34" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000035"), Name = "Book 35" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000036"), Name = "Book 36" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000037"), Name = "Book 37" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000038"), Name = "Book 38" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000039"), Name = "Book 39" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000040"), Name = "Book 40" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000041"), Name = "Book 41" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000042"), Name = "Book 42" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000043"), Name = "Book 43" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000044"), Name = "Book 44" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000045"), Name = "Book 45" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000046"), Name = "Book 46" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000047"), Name = "Book 47" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000048"), Name = "Book 48" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000049"), Name = "Book 49" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000050"), Name = "Book 50" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000051"), Name = "Book 51" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000052"), Name = "Book 52" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000053"), Name = "Book 53" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000054"), Name = "Book 54" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000055"), Name = "Book 55" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000056"), Name = "Book 56" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000057"), Name = "Book 57" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000058"), Name = "Book 58" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000059"), Name = "Book 59" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000060"), Name = "Book 60" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000061"), Name = "Book 61" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000062"), Name = "Book 62" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000063"), Name = "Book 63" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000064"), Name = "Book 64" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000065"), Name = "Book 65" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000066"), Name = "Book 66" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000067"), Name = "Book 67" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000068"), Name = "Book 68" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000069"), Name = "Book 69" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000070"), Name = "Book 70" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000071"), Name = "Book 71" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000072"), Name = "Book 72" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000073"), Name = "Book 73" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000074"), Name = "Book 74" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000075"), Name = "Book 75" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000076"), Name = "Book 76" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000077"), Name = "Book 77" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000078"), Name = "Book 78" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000079"), Name = "Book 79" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000080"), Name = "Book 80" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000081"), Name = "Book 81" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000082"), Name = "Book 82" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000083"), Name = "Book 83" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000084"), Name = "Book 84" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000085"), Name = "Book 85" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000086"), Name = "Book 86" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000087"), Name = "Book 87" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000088"), Name = "Book 88" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000089"), Name = "Book 89" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000090"), Name = "Book 90" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000091"), Name = "Book 91" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000092"), Name = "Book 92" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000093"), Name = "Book 93" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000094"), Name = "Book 94" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000095"), Name = "Book 95" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000096"), Name = "Book 96" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000097"), Name = "Book 97" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000098"), Name = "Book 98" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000099"), Name = "Book 99" },
            new Book { BookId = Guid.Parse("00000000-0000-0000-0000-000000000100"), Name = "Book 100" }
        };
    }


    public static IEnumerable GetAddBookTestCaseData()
    {
        var book = new Book
        {
            BookId = Guid.Parse("BCF316DA-9D2A-4412-9DF2-77FC0B6A2CBF"),
            Name = "Add book 1"
        };
        var result = new TestCaseData(book).SetName(book.Name);
        yield return result;

        book = new Book
        {
            BookId = Guid.Parse("BFFCEA05-0327-4E88-87C3-90E1F294485B"),
            Name = "Add book 2"
        };
        result = new TestCaseData(book).SetName(book.Name);
        yield return result;

        book = new Book
        {
            BookId = Guid.Parse("6AAB913F-CA8E-46AB-A08F-807B5C5EE552"),
            Name = "Add book 3"
        };
        result = new TestCaseData(book).SetName(book.Name);
        yield return result;
    }

    public static IEnumerable GetRemoveBookTestCaseData()
    {
        var bookData = GetBookData();
        var bookIdToRemove = bookData[0].BookId;
        var result = new TestCaseData(bookIdToRemove).SetName($"Remove book id {bookIdToRemove}");
        yield return result;

        bookIdToRemove = bookData[1].BookId;
        result = new TestCaseData(bookIdToRemove).SetName($"Remove book id {bookIdToRemove}");
        yield return result;

        bookIdToRemove = bookData[2].BookId;
        result = new TestCaseData(bookIdToRemove).SetName($"Remove book id {bookIdToRemove}");
        yield return result;
    }

    public static IEnumerable GetUnAttachedRemoveBookTestCaseData()
    {
        var bookData = GetBookData();
        var bookToRemove = new Book
        {
            BookId = bookData[0].BookId,
            Name = bookData[0].Name
        };
        var result = new TestCaseData(bookToRemove).SetName($"Remove unattached book id {bookToRemove.BookId}");
        yield return result;

        bookToRemove = new Book
        {
            BookId = bookData[1].BookId,
            Name = bookData[1].Name
        };
        result = new TestCaseData(bookToRemove).SetName($"Remove unattached book id {bookToRemove.BookId}");
        yield return result;

        bookToRemove = new Book
        {
            BookId = bookData[2].BookId,
            Name = bookData[2].Name
        };
        result = new TestCaseData(bookToRemove).SetName($"Remove unattached book id {bookToRemove.BookId}");
        yield return result;
    }

    public static IEnumerable GetUpdateBookTestCaseData()
    {
        var bookData = GetBookData();
        var bookToUpdate = bookData[0];
        bookToUpdate.Name = "my lovely book";
        var result = new TestCaseData(bookToUpdate).SetName($"Updating book id {bookToUpdate.BookId}");
        yield return result;

        bookToUpdate = bookData[1];
        bookToUpdate.Name = "new book";
        result = new TestCaseData(bookToUpdate).SetName($"Updating book id {bookToUpdate.BookId}");
        yield return result;

        bookToUpdate = bookData[2];
        bookToUpdate.Name = "old book";
        result = new TestCaseData(bookToUpdate).SetName($"Updating book id {bookToUpdate.BookId}");
        yield return result;
    }

    public static IEnumerable GetUnattachedUpdateBookTestCaseData()
    {
        var bookData = GetBookData();
        var bookToUpdate = new Book
        {
            BookId = bookData[0].BookId,
            Name = "my lovely book"
        };
        var result = new TestCaseData(bookToUpdate).SetName($"Updating unattached book id {bookToUpdate.BookId}");
        yield return result;

        bookToUpdate = new Book
        {
            BookId = bookData[1].BookId,
            Name = "new book"
        };
        result = new TestCaseData(bookToUpdate).SetName($"Updating book id {bookToUpdate.BookId}");
        yield return result;

        bookToUpdate = new Book
        {
            BookId = bookData[2].BookId,
            Name = "old book"
        };
        result = new TestCaseData(bookToUpdate).SetName($"Updating book id {bookToUpdate.BookId}");
        yield return result;
    }

    public static IEnumerable GetFirstOrDefaultBookTestCaseData()
    {
        var bookData = GetBookData();
        var bookIdToGet = bookData[3].BookId;
        var result = new TestCaseData(bookIdToGet).SetName($"Get book id {bookIdToGet}");
        yield return result;

        bookIdToGet = bookData[2].BookId;
        result = new TestCaseData(bookIdToGet).SetName($"Get book id {bookIdToGet}");
        yield return result;

        bookIdToGet = bookData[3].BookId;
        result = new TestCaseData(bookIdToGet).SetName($"Get book id {bookIdToGet}");
        yield return result;
    }

    public static IEnumerable GetListBooksTestCaseData()
    {
        var searchString = "Book";
        var result = new TestCaseData(searchString).SetName($"Search for books with containing {searchString}");
        yield return result;

        searchString = "Novel";
        result = new TestCaseData(searchString).SetName($"Search for books with containing {searchString}");
        yield return result;

        searchString = "i";
        result = new TestCaseData(searchString).SetName($"Search for books with containing {searchString}");
        yield return result;
    }

    public static IEnumerable PagedBookTestCaseData()
    {
        var data = GetPagedBookData();
        var searchTerm = "Book";
        var pagingRequest = new PagingRequest { PageIndex = 0, PageSize = 10 };
        var query = data.Where(d => d.Name.Contains(searchTerm));
        var expectedTotalCount = query.Count();
        var expectedData = query
            .Skip(pagingRequest.PageSize * pagingRequest.PageIndex)
            .Take(pagingRequest.PageSize)
            .ToList();
        var result =
            new TestCaseData(searchTerm, pagingRequest, expectedData, expectedTotalCount)
                .SetName($"Search term = {searchTerm}, page size = {pagingRequest.PageSize}, page index = {pagingRequest.PageIndex}, expected total count = {expectedTotalCount}");
        yield return result;

        data = GetPagedBookData();
        searchTerm = "Book";
        pagingRequest = new PagingRequest { PageIndex = 1, PageSize = 30 };
        query = data.Where(d => d.Name.Contains(searchTerm));
        expectedTotalCount = query.Count();
        expectedData = query
            .Skip(pagingRequest.PageSize * pagingRequest.PageIndex)
            .Take(pagingRequest.PageSize)
            .ToList();
        result =
            new TestCaseData(searchTerm, pagingRequest, expectedData, expectedTotalCount)
                .SetName($"Search term = {searchTerm}, page size = {pagingRequest.PageSize}, page index = {pagingRequest.PageIndex}, expected total count = {expectedTotalCount}");
        yield return result;

        data = GetPagedBookData();
        searchTerm = "Book";
        pagingRequest = new PagingRequest { PageIndex = 9, PageSize = 10 };
        query = data.Where(d => d.Name.Contains(searchTerm));
        expectedTotalCount = query.Count();
        expectedData = query
            .Skip(pagingRequest.PageSize * pagingRequest.PageIndex)
            .Take(pagingRequest.PageSize)
            .ToList();
        result =
            new TestCaseData(searchTerm, pagingRequest, expectedData, expectedTotalCount)
                .SetName($"Search term = {searchTerm}, page size = {pagingRequest.PageSize}, page index = {pagingRequest.PageIndex}, expected total count = {expectedTotalCount}");
        yield return result;

        data = GetPagedBookData();
        searchTerm = "1";
        pagingRequest = new PagingRequest { PageIndex = 0, PageSize = 10 };
        query = data.Where(d => d.Name.Contains(searchTerm));
        expectedTotalCount = query.Count();
        expectedData = query
            .Skip(pagingRequest.PageSize * pagingRequest.PageIndex)
            .Take(pagingRequest.PageSize)
            .ToList();
        result =
            new TestCaseData(searchTerm, pagingRequest, expectedData, expectedTotalCount)
                .SetName($"Search term = {searchTerm}, page size = {pagingRequest.PageSize}, page index = {pagingRequest.PageIndex}, expected total count = {expectedTotalCount}");
        yield return result;
    }
}