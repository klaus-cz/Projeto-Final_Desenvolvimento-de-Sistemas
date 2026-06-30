namespace ChecklistProductivity.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public ICollection<Checklist> Checklists { get; private set; } = new List<Checklist>();

    private User() { }

    public User(string name, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("O nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("O e-mail é obrigatório.");
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("A senha criptografada é obrigatória.");

        Name = name.Trim();
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
    }
}
