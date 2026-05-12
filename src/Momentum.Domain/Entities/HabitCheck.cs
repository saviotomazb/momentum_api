using Momentum.Domain.Common;

namespace Momentum.Domain.Entities;

public class HabitCheck : BaseEntity
{
    public Guid HabitId { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public Habit Habit { get; set; } = null!;
}