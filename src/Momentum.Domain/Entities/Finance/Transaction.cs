using Momentum.Domain.Common;
using Momentum.Domain.Enums.Finance;

namespace Momentum.Domain.Entities.Finance
{
    public class Transaction : BaseEntity
    {
        public string Description { get; private set; } = null!;

        public decimal Amount { get; private set; }

        public TransactionType Type { get; private set; }

        public TransactionFrequency Frequency { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public Guid CategoryId { get; private set; }

        public Category Category { get; private set; } = null!;

        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;
    }
}