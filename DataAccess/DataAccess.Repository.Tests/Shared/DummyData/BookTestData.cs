using System.Collections;
using DataAccess.Repository.Tests.Shared.Entities;
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
}