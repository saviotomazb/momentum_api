using Momentum.Domain.Enums;

namespace Momentum.Application.DTOs.Habits;

public class HabitResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public HabitFrequency Frequency { get; set; }

    public bool CompletedToday { get; set; }

    public int CurrentStreak { get; set; }

    public int TotalCompletions { get; set; }

    public DateTime CreatedAt { get; set; }
}