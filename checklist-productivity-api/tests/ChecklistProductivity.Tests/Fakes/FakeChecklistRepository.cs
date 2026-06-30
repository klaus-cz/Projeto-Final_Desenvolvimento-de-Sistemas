using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Tests.Fakes;

public class FakeChecklistRepository : IChecklistRepository
{
    public List<Checklist> Checklists { get; } = new();

    public Task<IReadOnlyCollection<Checklist>> ListByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Checklist> result = Checklists.Where(x => x.UserId == userId).ToList();
        return Task.FromResult(result);
    }

    public Task<Checklist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Checklists.FirstOrDefault(x => x.Id == id));
    }

    public Task AddAsync(Checklist checklist, CancellationToken cancellationToken = default)
    {
        Checklists.Add(checklist);
        return Task.CompletedTask;
    }

    public void Remove(Checklist checklist)
    {
        Checklists.Remove(checklist);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
