namespace ChecklistProductivity.Domain.Entities;

public class ChecklistItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ChecklistId { get; private set; }
    public Checklist? Checklist { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    public int Position { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; private set; }

    private ChecklistItem() { }

    public ChecklistItem(Guid checklistId, string title, int position)
    {
        if (checklistId == Guid.Empty) throw new ArgumentException("Checklist inválido.");
        ValidateTitle(title);
        if (position < 0) throw new ArgumentException("A posição não pode ser negativa.");

        ChecklistId = checklistId;
        Title = title.Trim();
        Position = position;
    }

    public void Update(string title, bool isCompleted, int position)
    {
        ValidateTitle(title);
        if (position < 0) throw new ArgumentException("A posição não pode ser negativa.");

        Title = title.Trim();
        Position = position;
        SetCompletion(isCompleted);
    }

    public void SetCompletion(bool isCompleted)
    {
        IsCompleted = isCompleted;
        CompletedAt = isCompleted ? DateTime.UtcNow : null;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("O título do item é obrigatório.");
        if (title.Length > 160) throw new ArgumentException("O título do item deve ter no máximo 160 caracteres.");
    }
}
