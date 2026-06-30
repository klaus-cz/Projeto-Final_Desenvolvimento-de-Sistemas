using ChecklistProductivity.Application.DTOs;
using ChecklistProductivity.Application.Exceptions;
using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository users, IPasswordHasherService passwordHasher, ITokenService tokenService)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        ValidateRegister(request);

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var existingUser = await _users.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (existingUser is not null)
            throw new ConflictException("Já existe um usuário cadastrado com este e-mail.");

        var passwordHash = _passwordHasher.Hash(request.Password);
        var user = new User(request.Name, normalizedEmail, passwordHash);

        await _users.AddAsync(user, cancellationToken);

        return ToAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            throw new ValidationAppException("E-mail e senha são obrigatórios.");

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await _users.GetByEmailAsync(normalizedEmail, cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAppException("E-mail ou senha inválidos.");

        return ToAuthResponse(user);
    }

    private AuthResponse ToAuthResponse(User user)
    {
        var token = _tokenService.Generate(user);
        return new AuthResponse(token, user.Id, user.Name, user.Email);
    }

    private static void ValidateRegister(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationAppException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains('@'))
            throw new ValidationAppException("Informe um e-mail válido.");

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
            throw new ValidationAppException("A senha deve ter pelo menos 8 caracteres.");
    }
}
