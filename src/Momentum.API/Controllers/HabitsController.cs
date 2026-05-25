using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.DTOs.Habits;
using Momentum.Application.Interfaces.Habits;

namespace Momentum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HabitsController : ControllerBase
{
    private readonly IHabitService _habitService;

    public HabitsController(IHabitService habitService)
    {
        _habitService = habitService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();

        var habits = await _habitService.GetAllAsync(userId);

        return Ok(habits);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();

        var habit = await _habitService.GetByIdAsync(id, userId);

        if (habit is null)
            return NotFound();

        return Ok(habit);
    }

    [HttpPost]
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
    public async Task<IActionResult> Complete(Guid id)
    {
        var userId = GetUserId();

        var completed = await _habitService.CompleteAsync(
            id,
            userId);

        if (!completed)
            return BadRequest(
                new { message = "Habit already completed today." });

        return Ok(new
        {
            message = "Habit completed successfully."
        });
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!);
    }
}