namespace DataAccess.Repository.Tests.Shared.Entities;

public class Book
{
    public Guid BookId { get; set; }

    public string Name { get; set; }

    public Book()
    {
        BookId = Guid.Empty;
        Name = string.Empty;
    }
}