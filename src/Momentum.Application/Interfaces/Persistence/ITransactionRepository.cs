
namespace Momentum.Application.Interfaces.Persistence
{
    public class ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);

        Task<Transaction?> GetByIdAsync(Guid id);

        Task<IEnumerable<Transaction>> GetAllByUserAsync(Guid userId);

        Task UpdateAsync(Transaction transaction);

        Task DeleteAsync(Transaction transaction);        
    }
}