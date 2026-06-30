using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}

public interface IChecklistRepository
{
    Task<IReadOnlyCollection<Checklist>> ListByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Checklist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Checklist checklist, CancellationToken cancellationToken = default);
    void Remove(Checklist checklist);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
