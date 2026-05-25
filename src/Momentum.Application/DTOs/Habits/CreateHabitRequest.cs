using Momentum.Domain.Enums;

namespace Momentum.Application.DTOs.Habits;

public class CreateHabitRequest
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public HabitFrequency Frequency { get; set; }
}