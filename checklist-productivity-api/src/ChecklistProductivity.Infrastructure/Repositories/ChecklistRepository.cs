using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Domain.Entities;
using ChecklistProductivity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChecklistProductivity.Infrastructure.Repositories;

public class ChecklistRepository : IChecklistRepository
{
    private readonly AppDbContext _db;

    public ChecklistRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyCollection<Checklist>> ListByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _db.Checklists
            .Include(x => x.Items)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<Checklist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _db.Checklists
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Checklist checklist, CancellationToken cancellationToken = default)
    {
        _db.Checklists.Add(checklist);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public void Remove(Checklist checklist)
    {
        _db.Checklists.Remove(checklist);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _db.SaveChangesAsync(cancellationToken);
    }
}
