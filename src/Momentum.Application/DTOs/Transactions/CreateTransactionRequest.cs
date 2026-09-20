using Momentum.Domain.Enums.Finance;

namespace Momentum.Application.DTOs.Transactions
{
    public class CreateTransactionRequest
    {
        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public TransactionFrequency Frequency { get; set; }

        public DateOnly TransactionDate { get; set; }

        public Guid CategoryId { get; set; }        
    }
}