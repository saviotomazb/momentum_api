using Momentum.Application.DTOs.Habits;
using Momentum.Application.Interfaces.Habits;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Domain.Entities;

namespace Momentum.Application.Services.Habits;

public class HabitService : IHabitService
{
    private readonly IHabitRepository _habitRepository;

    public HabitService(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task<List<HabitResponse>> GetAllAsync(Guid userId)
    {
        var habits = await _habitRepository
            .GetAllByUserIdAsync(userId);

        return habits.Select(MapToResponse).ToList();
    }

    public async Task<HabitResponse?> GetByIdAsync(
        Guid id,
        Guid userId)
    {
        var habit = await _habitRepository
            .GetByIdWithChecksAsync(id);

        if (habit is null || habit.UserId != userId)
            return null;

        return MapToResponse(habit);
    }

    public async Task<HabitResponse> CreateAsync(
        Guid userId,
        CreateHabitRequest request)
    {
        var habit = new Habit
        {
            UserId = userId,
            Title = request.Title,
            Description = request.Description,
            Frequency = request.Frequency
        };

        await _habitRepository.CreateAsync(habit);

        return MapToResponse(habit);
    }

    public async Task<HabitResponse?> UpdateAsync(
        Guid id,
        Guid userId,
        UpdateHabitRequest request)
    {
        var habit = await _habitRepository.GetByIdAsync(id);

        if (habit is null || habit.UserId != userId)
            return null;

        habit.Title = request.Title;
        habit.Description = request.Description;
        habit.Frequency = request.Frequency;

        await _habitRepository.UpdateAsync(habit);

        return MapToResponse(habit);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var habit = await _habitRepository.GetByIdAsync(id);

        if (habit is null || habit.UserId != userId)
            return false;

        await _habitRepository.DeleteAsync(habit);

        return true;
    }

    public async Task<bool> CompleteAsync(Guid id, Guid userId)
    {
        var habit = await _habitRepository
            .GetByIdWithChecksAsync(id);

        if (habit is null || habit.UserId != userId)
            return false;

        var alreadyCompletedToday = habit.HabitChecks.Any(x =>
            x.CompletedAt.Date == DateTime.UtcNow.Date);

        if (alreadyCompletedToday)
            return false;

        habit.HabitChecks.Add(new HabitCheck
        {
            HabitId = habit.Id,
            CompletedAt = DateTime.UtcNow
        });

        await _habitRepository.UpdateAsync(habit);

        return true;
    }

    private static HabitResponse MapToResponse(Habit habit)
    {
        var completedToday = habit.HabitChecks.Any(x =>
            x.CompletedAt.Date == DateTime.UtcNow.Date);

        return new HabitResponse
        {
            Id = habit.Id,
            Title = habit.Title,
            Description = habit.Description,
            Frequency = habit.Frequency,
            CompletedToday = completedToday,
            CurrentStreak = CalculateStreak(habit),
            TotalCompletions = habit.HabitChecks.Count,
            CreatedAt = habit.CreatedAt
        };
    }

    private static int CalculateStreak(Habit habit)
    {
        var dates = habit.HabitChecks
            .Select(x => x.CompletedAt.Date)
            .Distinct()
            .OrderByDescending(x => x)
            .ToList();

        if (!dates.Any())
            return 0;

        var streak = 0;
        var currentDate = DateTime.UtcNow.Date;

        foreach (var date in dates)
        {
            if (date == currentDate)
            {
                streak++;
                currentDate = currentDate.AddDays(-1);
            }
            else
            {
                break;
            }
        }

        return streak;
    }
}