using ChecklistProductivity.Application.DTOs;

namespace ChecklistProductivity.Application.Interfaces;

public interface IChecklistService
{
    Task<IReadOnlyCollection<ChecklistResponse>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ChecklistResponse> GetByIdAsync(Guid userId, Guid checklistId, CancellationToken cancellationToken = default);
    Task<ChecklistResponse> CreateAsync(Guid userId, CreateChecklistRequest request, CancellationToken cancellationToken = default);
    Task<ChecklistResponse> UpdateAsync(Guid userId, Guid checklistId, UpdateChecklistRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid userId, Guid checklistId, CancellationToken cancellationToken = default);
    Task<ChecklistResponse> AddItemAsync(Guid userId, Guid checklistId, CreateChecklistItemRequest request, CancellationToken cancellationToken = default);
    Task<ChecklistResponse> UpdateItemAsync(Guid userId, Guid checklistId, Guid itemId, UpdateChecklistItemRequest request, CancellationToken cancellationToken = default);
    Task<ChecklistResponse> DeleteItemAsync(Guid userId, Guid checklistId, Guid itemId, CancellationToken cancellationToken = default);
}
