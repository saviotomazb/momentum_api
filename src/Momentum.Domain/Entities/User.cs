using Momentum.Domain.Common;
using Momentum.Domain.Entities.Finance;

namespace Momentum.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Habit> Habits { get; set; } = new List<Habit>();

    public ICollection<Category> Categories { get; private set; } = [];

    public ICollection<Transaction> Transactions { get; private set; } = [];
}