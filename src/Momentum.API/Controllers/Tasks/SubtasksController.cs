using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.DTOs.Tasks.Subtask;
using Momentum.Application.Interfaces.Tasks;
using System.Security.Claims;

namespace Momentum.API.Controllers.Tasks;

[ApiController]
[Route("api/subtasks")]
[Authorize]
public class SubtasksController : ControllerBase
{
    private readonly ISubtaskService _subtaskService;

    public SubtasksController(ISubtaskService subtaskService)
    {
        _subtaskService = subtaskService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SubtaskResponse>> GetById(
        Guid id)
    {
        var userId = GetUserId();

        var subtask = await _subtaskService
            .GetByIdAsync(userId, id);

        return Ok(subtask);
    }

    [HttpGet("task/{taskId:guid}")]
    public async Task<ActionResult<IEnumerable<SubtaskResponse>>> GetAllByTaskId(
        Guid taskId)
    {
        var userId = GetUserId();

        var subtasks = await _subtaskService
            .GetAllByTaskIdAsync(userId, taskId);

        return Ok(subtasks);
    }

    [HttpPost]
    public async Task<ActionResult<SubtaskResponse>> Create(
        CreateSubtaskRequest request)
    {
        var userId = GetUserId();

        var subtask = await _subtaskService
            .CreateAsync(userId, request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = subtask.Id },
            subtask);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SubtaskResponse>> Update(
        Guid id,
        UpdateSubtaskRequest request)
    {
        var userId = GetUserId();

        var subtask = await _subtaskService
            .UpdateAsync(userId, id, request);

        return Ok(subtask);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id)
    {
        var userId = GetUserId();

        await _subtaskService
            .DeleteAsync(userId, id);

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!);
    }

}