namespace ChecklistProductivity.Application.DTOs;

public record CreateChecklistRequest(string Title, string? Description, DateTime? DueDate);
public record UpdateChecklistRequest(string Title, string? Description, DateTime? DueDate, bool IsArchived);

public record ChecklistResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsArchived,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IReadOnlyCollection<ChecklistItemResponse> Items
);

public record CreateChecklistItemRequest(string Title, int Position);
public record UpdateChecklistItemRequest(string Title, bool IsCompleted, int Position);

public record ChecklistItemResponse(
    Guid Id,
    string Title,
    bool IsCompleted,
    int Position,
    DateTime CreatedAt,
    DateTime? CompletedAt
);
