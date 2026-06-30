using ChecklistProductivity.Application.DTOs;
using ChecklistProductivity.Application.Exceptions;
using ChecklistProductivity.Application.Services;
using ChecklistProductivity.Tests.Fakes;
using Xunit;

namespace ChecklistProductivity.Tests;

public class ChecklistServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateChecklist_ForAuthenticatedUser()
    {
        var repository = new FakeChecklistRepository();
        var service = new ChecklistService(repository);
        var userId = Guid.NewGuid();

        var result = await service.CreateAsync(userId, new CreateChecklistRequest("Estudar API", "Finalizar projeto", null));

        Assert.Equal("Estudar API", result.Title);
        Assert.Single(repository.Checklists);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowForbidden_WhenChecklistBelongsToAnotherUser()
    {
        var repository = new FakeChecklistRepository();
        var service = new ChecklistService(repository);
        var ownerId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();

        var created = await service.CreateAsync(ownerId, new CreateChecklistRequest("Privado", null, null));

        await Assert.ThrowsAsync<ForbiddenAppException>(() =>
            service.GetByIdAsync(otherUserId, created.Id));
    }

    [Fact]
    public async Task AddItemAsync_ShouldAddItemToChecklist()
    {
        var repository = new FakeChecklistRepository();
        var service = new ChecklistService(repository);
        var userId = Guid.NewGuid();

        var checklist = await service.CreateAsync(userId, new CreateChecklistRequest("Rotina", null, null));
        var updated = await service.AddItemAsync(userId, checklist.Id, new CreateChecklistItemRequest("Beber água", 1));

        Assert.Single(updated.Items);
        Assert.Equal("Beber água", updated.Items.First().Title);
    }
}
