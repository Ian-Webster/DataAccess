using DataAccess.Example.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Example.Data.EntityTypeMappings;

public class BookEntityTypeMapping: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(pk => pk.BookId);

        builder.ToTable(nameof(Book));
    }
}