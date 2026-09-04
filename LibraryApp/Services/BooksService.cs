using LibraryApp.Data;
using LibraryApp.Model;
using LibraryApp.View_Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class BooksService
{
    private readonly AppDbContext _context;
    public BooksService(AppDbContext context) => _context = context;

    public Task<List<Book>> GetAllAsync(string? search = null, CancellationToken token = default)
    {
        var query = _context.Books.AsNoTracking()
            .Include(book => book.PublisherNV)
            .Include(book => book.bookAuthors).ThenInclude(link => link.authorNV)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(book => book.Title.Contains(search.Trim()) || book.Description.Contains(search.Trim()));
        return query.OrderBy(book => book.Title).ToListAsync(token);
    }

    public Task<Book?> GetByIdAsync(int id, CancellationToken token = default) =>
        _context.Books.AsNoTracking()
            .Include(book => book.PublisherNV)
            .Include(book => book.bookAuthors).ThenInclude(link => link.authorNV)
            .FirstOrDefaultAsync(book => book.Id == id, token);

    public async Task<Book> CreateAsync(BookVm request, CancellationToken token = default)
    {
        await ValidateRelationsAsync(request, token);
        var book = Map(request, new Book { AddedDate = DateTime.UtcNow });
        book.bookAuthors = request.AuthorsId.Distinct()
            .Select(authorId => new BookAuthor { AuthorId = authorId }).ToList();
        _context.Books.Add(book);
        await _context.SaveChangesAsync(token);
        return book;
    }

    public async Task<Book?> UpdateAsync(int id, BookVm request, CancellationToken token = default)
    {
        await ValidateRelationsAsync(request, token);
        var book = await _context.Books.Include(item => item.bookAuthors)
            .FirstOrDefaultAsync(item => item.Id == id, token);
        if (book is null) return null;

        Map(request, book);
        book.UpddatedDate = DateTime.UtcNow;
        _context.BookAuthors.RemoveRange(book.bookAuthors);
        book.bookAuthors = request.AuthorsId.Distinct()
            .Select(authorId => new BookAuthor { BookId = id, AuthorId = authorId }).ToList();
        await _context.SaveChangesAsync(token);
        return book;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var book = await _context.Books.FindAsync([id], token);
        if (book is null) return false;
        _context.Books.Remove(book);
        await _context.SaveChangesAsync(token);
        return true;
    }

    private async Task ValidateRelationsAsync(BookVm request, CancellationToken token)
    {
        if (request.PublisherId is int publisherId &&
            !await _context.Publishers.AnyAsync(item => item.Id == publisherId, token))
            throw new ArgumentException("The selected publisher does not exist.");

        var ids = request.AuthorsId.Distinct().ToList();
        var existing = await _context.Authors.CountAsync(item => ids.Contains(item.Id), token);
        if (existing != ids.Count)
            throw new ArgumentException("One or more selected authors do not exist.");
    }

    private static Book Map(BookVm request, Book book)
    {
        book.Title = request.Title.Trim();
        book.Price = request.Price;
        book.Description = request.Description.Trim();
        book.Author = request.Author?.Trim() ?? string.Empty;
        book.IsRead = request.IsRead;
        book.ReadDate = request.IsRead ? request.ReadDate : null;
        book.Rate = request.Rate;
        book.Genre = request.Genre.Trim();
        book.CoverURl = request.CoverUrl?.Trim() ?? string.Empty;
        book.BookURl = request.BookUrl?.Trim() ?? string.Empty;
        book.PublisherId = request.PublisherId;
        return book;
    }
}
