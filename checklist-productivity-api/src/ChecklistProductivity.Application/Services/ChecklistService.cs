using ChecklistProductivity.Application.DTOs;
using ChecklistProductivity.Application.Exceptions;
using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Domain.Entities;

namespace ChecklistProductivity.Application.Services;

public class ChecklistService : IChecklistService
{
    private readonly IChecklistRepository _checklists;

    public ChecklistService(IChecklistRepository checklists)
    {
        _checklists = checklists;
    }

    public async Task<IReadOnlyCollection<ChecklistResponse>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var checklists = await _checklists.ListByUserAsync(userId, cancellationToken);
        return checklists.Select(ToResponse).ToList();
    }

    public async Task<ChecklistResponse> GetByIdAsync(Guid userId, Guid checklistId, CancellationToken cancellationToken = default)
    {
        var checklist = await GetOwnedChecklistAsync(userId, checklistId, cancellationToken);
        return ToResponse(checklist);
    }

    public async Task<ChecklistResponse> CreateAsync(Guid userId, CreateChecklistRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ValidationAppException("O título do checklist é obrigatório.");

        var checklist = new Checklist(userId, request.Title, request.Description, request.DueDate);
        await _checklists.AddAsync(checklist, cancellationToken);

        return ToResponse(checklist);
    }

    public async Task<ChecklistResponse> UpdateAsync(Guid userId, Guid checklistId, UpdateChecklistRequest request, CancellationToken cancellationToken = default)
    {
        var checklist = await GetOwnedChecklistAsync(userId, checklistId, cancellationToken);
        checklist.Update(request.Title, request.Description, request.DueDate, request.IsArchived);
        await _checklists.SaveChangesAsync(cancellationToken);
        return ToResponse(checklist);
    }

    public async Task DeleteAsync(Guid userId, Guid checklistId, CancellationToken cancellationToken = default)
    {
        var checklist = await GetOwnedChecklistAsync(userId, checklistId, cancellationToken);
        _checklists.Remove(checklist);
        await _checklists.SaveChangesAsync(cancellationToken);
    }

    public async Task<ChecklistResponse> AddItemAsync(Guid userId, Guid checklistId, CreateChecklistItemRequest request, CancellationToken cancellationToken = default)
    {
        var checklist = await GetOwnedChecklistAsync(userId, checklistId, cancellationToken);
        checklist.AddItem(request.Title, request.Position);
        await _checklists.SaveChangesAsync(cancellationToken);
        return ToResponse(checklist);
    }

    public async Task<ChecklistResponse> UpdateItemAsync(Guid userId, Guid checklistId, Guid itemId, UpdateChecklistItemRequest request, CancellationToken cancellationToken = default)
    {
        var checklist = await GetOwnedChecklistAsync(userId, checklistId, cancellationToken);
        var item = checklist.Items.FirstOrDefault(x => x.Id == itemId);
        if (item is null) throw new NotFoundException("Item não encontrado.");

        item.Update(request.Title, request.IsCompleted, request.Position);
        await _checklists.SaveChangesAsync(cancellationToken);
        return ToResponse(checklist);
    }

    public async Task<ChecklistResponse> DeleteItemAsync(Guid userId, Guid checklistId, Guid itemId, CancellationToken cancellationToken = default)
    {
        var checklist = await GetOwnedChecklistAsync(userId, checklistId, cancellationToken);
        var item = checklist.Items.FirstOrDefault(x => x.Id == itemId);
        if (item is null) throw new NotFoundException("Item não encontrado.");

        checklist.RemoveItem(item);
        await _checklists.SaveChangesAsync(cancellationToken);
        return ToResponse(checklist);
    }

    private async Task<Checklist> GetOwnedChecklistAsync(Guid userId, Guid checklistId, CancellationToken cancellationToken)
    {
        var checklist = await _checklists.GetByIdAsync(checklistId, cancellationToken);
        if (checklist is null) throw new NotFoundException("Checklist não encontrado.");
        if (checklist.UserId != userId) throw new ForbiddenAppException("Você não tem permissão para acessar este checklist.");
        return checklist;
    }

    private static ChecklistResponse ToResponse(Checklist checklist)
    {
        return new ChecklistResponse(
            checklist.Id,
            checklist.Title,
            checklist.Description,
            checklist.DueDate,
            checklist.IsArchived,
            checklist.CreatedAt,
            checklist.UpdatedAt,
            checklist.Items
                .OrderBy(x => x.Position)
                .Select(item => new ChecklistItemResponse(
                    item.Id,
                    item.Title,
                    item.IsCompleted,
                    item.Position,
                    item.CreatedAt,
                    item.CompletedAt
                ))
                .ToList()
        );
    }
}
