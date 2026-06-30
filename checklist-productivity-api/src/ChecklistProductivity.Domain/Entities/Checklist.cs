namespace ChecklistProductivity.Domain.Entities;

public class Checklist
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public ICollection<ChecklistItem> Items { get; private set; } = new List<ChecklistItem>();

    private Checklist() { }

    public Checklist(Guid userId, string title, string? description, DateTime? dueDate)
    {
        if (userId == Guid.Empty) throw new ArgumentException("Usuário inválido.");
        ValidateTitle(title);

        UserId = userId;
        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        DueDate = dueDate;
    }

    public void Update(string title, string? description, DateTime? dueDate, bool isArchived)
    {
        ValidateTitle(title);
        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        DueDate = dueDate;
        IsArchived = isArchived;
        UpdatedAt = DateTime.UtcNow;
    }

    public ChecklistItem AddItem(string title, int position)
    {
        var item = new ChecklistItem(Id, title, position);
        Items.Add(item);
        UpdatedAt = DateTime.UtcNow;
        return item;
    }

    public void RemoveItem(ChecklistItem item)
    {
        Items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("O título é obrigatório.");
        if (title.Length > 120) throw new ArgumentException("O título deve ter no máximo 120 caracteres.");
    }
}
