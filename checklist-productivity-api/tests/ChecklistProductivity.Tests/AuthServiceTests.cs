using ChecklistProductivity.Application.DTOs;
using ChecklistProductivity.Application.Exceptions;
using ChecklistProductivity.Application.Services;
using ChecklistProductivity.Tests.Fakes;
using Xunit;

namespace ChecklistProductivity.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task RegisterAsync_ShouldCreateUser_WhenDataIsValid()
    {
        var users = new FakeUserRepository();
        var service = new AuthService(users, new FakePasswordHasherService(), new FakeTokenService());

        var result = await service.RegisterAsync(new RegisterRequest("Daniel", "daniel@email.com", "Senha@12345"));

        Assert.Equal("Daniel", result.Name);
        Assert.Equal("daniel@email.com", result.Email);
        Assert.Single(users.Users);
        Assert.False(string.IsNullOrWhiteSpace(result.Token));
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrowConflict_WhenEmailAlreadyExists()
    {
        var users = new FakeUserRepository();
        var service = new AuthService(users, new FakePasswordHasherService(), new FakeTokenService());

        await service.RegisterAsync(new RegisterRequest("Daniel", "daniel@email.com", "Senha@12345"));

        await Assert.ThrowsAsync<ConflictException>(() =>
            service.RegisterAsync(new RegisterRequest("Outro", "daniel@email.com", "Senha@12345")));
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorized_WhenPasswordIsWrong()
    {
        var users = new FakeUserRepository();
        var service = new AuthService(users, new FakePasswordHasherService(), new FakeTokenService());

        await service.RegisterAsync(new RegisterRequest("Daniel", "daniel@email.com", "Senha@12345"));

        await Assert.ThrowsAsync<UnauthorizedAppException>(() =>
            service.LoginAsync(new LoginRequest("daniel@email.com", "senha-errada")));
    }
}
