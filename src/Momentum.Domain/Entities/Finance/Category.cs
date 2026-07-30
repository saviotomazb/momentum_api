using Momentum.Domain.Common;

namespace Momentum.Domain.Entities.Finance
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public string Color { get; private set; } = string.Empty;

        public string Icon { get; private set; } = string.Empty;

        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;

        public ICollection<Transaction> Transactions { get; private set; } = [];
    
    }
}