using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.DTOs.Habits;
using Momentum.Application.Interfaces.Habits;

namespace Momentum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly IHabitService _habitService;

    public HabitsController(IHabitService habitService)
    {
        _habitService = habitService;
    }

    [HttpGet]
    [Authorize(Policy = "HabitsRead")]
    public async Task<IActionResult> GetAll([FromQuery] HabitFilterRequest filter)
    {
        var userId = GetUserId();

        var habits = await _habitService.GetAllAsync(userId, filter);

        return Ok(habits);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "HabitsRead")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();

        var habit = await _habitService.GetByIdAsync(id, userId);

        if (habit is null)
            return NotFound();

        return Ok(habit);
    }

    [HttpPost]
    [Authorize(Policy = "HabitsWrite")]
    public async Task<IActionResult> Create(
        CreateHabitRequest request)
    {
        var userId = GetUserId();

        var habit = await _habitService.CreateAsync(
            userId,
            request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = habit.Id },
            habit);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "HabitsWrite")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateHabitRequest request)
    {
        var userId = GetUserId();

        var habit = await _habitService.UpdateAsync(
            id,
            userId,
            request);

        if (habit is null)
            return NotFound();

        return Ok(habit);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "HabitsWrite")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();

        var deleted = await _habitService.DeleteAsync(
            id,
            userId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [Authorize(Policy = "HabitsWrite")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var userId = GetUserId();

        var habit = await _habitService.CompleteAsync(
            id,
            userId);

        if (habit is null)
            return BadRequest(
                new { message = "Habit already completed today." });

        return Ok(habit);
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!);
    }
}
