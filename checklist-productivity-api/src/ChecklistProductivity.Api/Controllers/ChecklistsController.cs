using ChecklistProductivity.Api.Extensions;
using ChecklistProductivity.Application.DTOs;
using ChecklistProductivity.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistProductivity.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/checklists")]
public class ChecklistsController : ControllerBase
{
    private readonly IChecklistService _checklistService;

    public ChecklistsController(IChecklistService checklistService)
    {
        _checklistService = checklistService;
    }

    /// <summary>Lista todos os checklists do usuário autenticado.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ChecklistResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ChecklistResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.GetAllAsync(userId, cancellationToken);
        return Ok(response);
    }

    /// <summary>Consulta um checklist específico do usuário autenticado.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ChecklistResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChecklistResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.GetByIdAsync(userId, id, cancellationToken);
        return Ok(response);
    }

    /// <summary>Cria um novo checklist.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ChecklistResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<ChecklistResponse>> Create(CreateChecklistRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.CreateAsync(userId, request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>Atualiza um checklist.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ChecklistResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ChecklistResponse>> Update(Guid id, UpdateChecklistRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.UpdateAsync(userId, id, request, cancellationToken);
        return Ok(response);
    }

    /// <summary>Remove um checklist.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        await _checklistService.DeleteAsync(userId, id, cancellationToken);
        return NoContent();
    }

    /// <summary>Adiciona um item dentro de um checklist.</summary>
    [HttpPost("{checklistId:guid}/items")]
    [ProducesResponseType(typeof(ChecklistResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ChecklistResponse>> AddItem(Guid checklistId, CreateChecklistItemRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.AddItemAsync(userId, checklistId, request, cancellationToken);
        return Ok(response);
    }

    /// <summary>Atualiza um item do checklist.</summary>
    [HttpPut("{checklistId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(typeof(ChecklistResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ChecklistResponse>> UpdateItem(Guid checklistId, Guid itemId, UpdateChecklistItemRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.UpdateItemAsync(userId, checklistId, itemId, request, cancellationToken);
        return Ok(response);
    }

    /// <summary>Remove um item do checklist.</summary>
    [HttpDelete("{checklistId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(typeof(ChecklistResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ChecklistResponse>> DeleteItem(Guid checklistId, Guid itemId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var response = await _checklistService.DeleteItemAsync(userId, checklistId, itemId, cancellationToken);
        return Ok(response);
    }
}
