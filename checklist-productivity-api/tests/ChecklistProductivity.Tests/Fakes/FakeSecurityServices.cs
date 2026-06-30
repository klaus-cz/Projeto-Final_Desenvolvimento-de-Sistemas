using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Tests.Fakes;

public class FakePasswordHasherService : IPasswordHasherService
{
    public string Hash(string password) => $"HASH::{password}";
    public bool Verify(string password, string passwordHash) => passwordHash == $"HASH::{password}";
}

public class FakeTokenService : ITokenService
{
    public string Generate(User user) => $"fake-token-for-{user.Id}";
}
