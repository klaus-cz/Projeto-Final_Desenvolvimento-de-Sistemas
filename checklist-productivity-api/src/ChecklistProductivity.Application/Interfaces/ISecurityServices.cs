using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Application.Interfaces;

public interface IPasswordHasherService
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}

public interface ITokenService
{
    string Generate(User user);
}
