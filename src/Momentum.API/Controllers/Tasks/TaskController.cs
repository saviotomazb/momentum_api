using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.DTOs.Tasks.Task;
using Momentum.Application.Interfaces.Tasks;
using System.Security.Claims;

namespace Momentum.API.Controllers.Tasks;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll()
    {
        var userId = GetUserId();

        var tasks = await _taskService
            .GetAllAsync(userId);

        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetById(
        Guid id)
    {
        var userId = GetUserId();

        var task = await _taskService
            .GetByIdAsync(userId, id);

        return Ok(task);
    }

    [HttpGet("list/{taskListId:guid}")]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> GetByTaskListId(
        Guid taskListId)
    {
        var userId = GetUserId();

        var tasks = await _taskService
            .GetByTaskListIdAsync(userId, taskListId);

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create(
        CreateTaskRequest request)
    {
        var userId = GetUserId();

        var task = await _taskService
            .CreateAsync(userId, request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> Update(
        Guid id,
        UpdateTaskRequest request)
    {
        var userId = GetUserId();

        var task = await _taskService
            .UpdateAsync(userId, id, request);

        return Ok(task);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id)
    {
        var userId = GetUserId();

        await _taskService
            .DeleteAsync(userId, id);

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!);
    }

}