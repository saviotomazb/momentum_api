using Momentum.Domain.Common;

namespace Momentum.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Habit> Habits { get; set; } = new List<Habit>();
}