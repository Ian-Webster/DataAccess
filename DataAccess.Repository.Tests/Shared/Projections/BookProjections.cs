using System.Linq.Expressions;
using DataAccess.Repository.Tests.Shared.DummyData;
using DataAccess.Repository.Tests.Shared.Entities;

namespace DataAccess.Repository.Tests.Shared.Projections;

public static class BookProjections
{
    public static Expression<Func<Book, ProjectedBook>> BookToBookProjected()
    {
        return b => new ProjectedBook
        {
            BookName = b.Name,
        };
    }
}