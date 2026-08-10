using Momentum.Application.DTOs.Transactions;

namespace Momentum.Application.Interfaces.Transactions
{
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateAsync(
            Guid userId,
            CreateTransactionRequest request);

        Task<TransactionResponse> GetByIdAsync(
            Guid userId,
            Guid id);

        Task<IEnumerable<TransactionResponse>> GetAllAsync(
            Guid userId);

        Task<TransactionResponse> UpdateAsync(
            Guid userId,
            Guid id,
            UpdateTransactionRequest request);

        Task DeleteAsync(
            Guid userId,
            Guid id);        
    }
}