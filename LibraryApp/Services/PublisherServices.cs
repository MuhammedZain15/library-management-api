using LibraryApp.Data;
using LibraryApp.Model;
using LibraryApp.View_Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class PublisherServices
{
    private readonly AppDbContext _context;
    public PublisherServices(AppDbContext context) => _context = context;

    public Task<List<Publisher>> GetAllAsync(string? search, CancellationToken token = default)
    {
        var query = _context.Publishers.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(x => x.Name.Contains(search.Trim()));
        return query.OrderBy(x => x.Name).ToListAsync(token);
    }

    public Task<Publisher?> GetByIdAsync(int id, CancellationToken token = default) =>
        _context.Publishers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);

    public async Task<Publisher> CreateAsync(PublisherVM request, CancellationToken token = default)
    {
        var entity = new Publisher { Name = request.Name.Trim() };
        _context.Publishers.Add(entity);
        await _context.SaveChangesAsync(token);
        return entity;
    }

    public async Task<Publisher?> UpdateAsync(int id, PublisherVM request, CancellationToken token = default)
    {
        var entity = await _context.Publishers.FindAsync([id], token);
        if (entity is null) return null;
        entity.Name = request.Name.Trim();
        await _context.SaveChangesAsync(token);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var entity = await _context.Publishers.FindAsync([id], token);
        if (entity is null) return false;
        _context.Publishers.Remove(entity);
        await _context.SaveChangesAsync(token);
        return true;
    }
}
