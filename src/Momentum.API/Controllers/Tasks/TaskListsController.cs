using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.DTOs.Tasks.TaskList;
using Momentum.Application.Interfaces.Tasks;
using System.Security.Claims;

namespace Momentum.API.Controllers.Tasks;

[ApiController]
[Route("api/task-lists")]
[Authorize]
public class TaskListsController : ControllerBase
{
    private readonly ITaskListService _taskListService;

    public TaskListsController(ITaskListService taskListService)
    {
        _taskListService = taskListService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskListResponse>>> GetAll()
    {
        var userId = GetUserId();

        var taskLists = await _taskListService
            .GetAllAsync(userId);

        return Ok(taskLists);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskListResponse>> GetById(
        Guid id)
    {
        var userId = GetUserId();

        var taskList = await _taskListService
            .GetByIdAsync(userId, id);

        return Ok(taskList);
    }

    [HttpPost]
    public async Task<ActionResult<TaskListResponse>> Create(
        CreateTaskListRequest request)
    {
        var userId = GetUserId();

        var taskList = await _taskListService
            .CreateAsync(userId, request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = taskList.Id },
            taskList);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskListResponse>> Update(
        Guid id,
        UpdateTaskListRequest request)
    {
        var userId = GetUserId();

        var taskList = await _taskListService
            .UpdateAsync(userId, id, request);

        return Ok(taskList);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id)
    {
        var userId = GetUserId();

        await _taskListService
            .DeleteAsync(userId, id);

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!);
    }

}