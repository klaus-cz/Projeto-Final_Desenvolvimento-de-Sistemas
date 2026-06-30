using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Tests.Fakes;

public class FakeUserRepository : IUserRepository
{
    public List<User> Users { get; } = new();

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return Task.FromResult(Users.FirstOrDefault(x => x.Email == normalized));
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Users.FirstOrDefault(x => x.Id == id));
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        Users.Add(user);
        return Task.CompletedTask;
    }
}
