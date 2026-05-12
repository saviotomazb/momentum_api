using Momentum.Domain.Common;
using Momentum.Domain.Enums;

namespace Momentum.Domain.Entities;

public class Habit : BaseEntity
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public HabitFrequency Frequency { get; set; }

    public User User { get; set; } = null!;

    public ICollection<HabitCheck> HabitChecks { get; set; } = new List<HabitCheck>();
}