using LibraryApp.Data;
using LibraryApp.Model;
using LibraryApp.View_Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class AuthorServices
{
    private readonly AppDbContext _context;
    public AuthorServices(AppDbContext context) => _context = context;

    public Task<List<Author>> GetAllAsync(string? search, CancellationToken token = default)
    {
        var query = _context.Authors.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(x => x.Name.Contains(search.Trim()));
        return query.OrderBy(x => x.Name).ToListAsync(token);
    }

    public Task<Author?> GetByIdAsync(int id, CancellationToken token = default) =>
        _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);

    public async Task<Author> CreateAsync(AuthorVm request, CancellationToken token = default)
    {
        var entity = new Author { Name = request.Name.Trim() };
        _context.Authors.Add(entity);
        await _context.SaveChangesAsync(token);
        return entity;
    }

    public async Task<Author?> UpdateAsync(int id, AuthorVm request, CancellationToken token = default)
    {
        var entity = await _context.Authors.FindAsync([id], token);
        if (entity is null) return null;
        entity.Name = request.Name.Trim();
        await _context.SaveChangesAsync(token);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var entity = await _context.Authors.FindAsync([id], token);
        if (entity is null) return false;
        _context.Authors.Remove(entity);
        await _context.SaveChangesAsync(token);
        return true;
    }
}
