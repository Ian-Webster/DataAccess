using DataAccess.Repository.Tests.Shared.Entities;

namespace DataAccess.Repository.Tests.Shared.Repositories;

public class BookRepository
{
    private readonly IRepository<Book> _bookRepo;

    public BookRepository(RepositoryFactory repositoryFactory)
    {
        _bookRepo = repositoryFactory.GetRepositoryByType<Book>();
    }

    public async Task<bool> AddBook(Book bookToAdd)
    {
        return await _bookRepo.Add(bookToAdd, new CancellationToken());
    }

    public async Task<IEnumerable<Book>?> GetAllBooks()
    {
        return await _bookRepo.List(p => true, new CancellationToken());
    }
}