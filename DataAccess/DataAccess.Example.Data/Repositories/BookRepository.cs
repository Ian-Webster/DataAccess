using DataAccess.Example.Data.Entities;
using DataAccess.Repository;

namespace DataAccess.Example.Data.Repositories;

public class BookRepository: IBookRepository
{
    private readonly IRepository<Book> _bookRepo;

    public BookRepository(RepositoryFactory repositoryFactory)
    {
        _bookRepo = repositoryFactory.GetRepositoryByType<Book>();
    }

    public async Task<Book?> GetBookById(Guid bookId, CancellationToken token)
    {
        return await _bookRepo.FirstOrDefault(b => b.BookId == bookId, token);
    }

    public async Task<IEnumerable<Book>?> GetAllBooks(CancellationToken token)
    {
        return await _bookRepo.List(p => true, token);
    }

    public async Task<bool> AddBook(Book bookToAdd, CancellationToken token)
    {
        if (await _bookRepo.Exists(b => b.BookId == bookToAdd.BookId, token))
        {
            return false;
        }

        return await _bookRepo.Add(bookToAdd, token);
    }

    public async Task<bool> UpdateBook(Book bookToUpdate, CancellationToken token)
    {
        if (!await _bookRepo.Exists(b => b.BookId == bookToUpdate.BookId, token))
        {
            return false;
        }

        return await _bookRepo.Update(bookToUpdate, token);
    }

    public async Task<bool> RemoveBook(Guid bookId, CancellationToken token)
    {
        var bookToRemove = await _bookRepo.FirstOrDefault(b => b.BookId == bookId, token);
        if (bookToRemove == null)
        {
            return false;
        }

        return await _bookRepo.Remove(bookToRemove, token);
    }
}