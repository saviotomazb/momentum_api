using Momentum.Application.DTOs.Transactions;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Application.Interfaces.Transactions;
using Momentum.Domain.Entities.Finance;

namespace Momentum.Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TransactionResponse> CreateAsync(
        Guid userId,
        CreateTransactionRequest request)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category is null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("Você não tem permissão para adicionar transações a esta categoria.");
            }

            var transaction = new Transaction(
            request.Description,
            request.Amount,
            request.Type,
            request.Frequency,
            request.TransactionDate,
            request.CategoryId,
            userId);

            await _transactionRepository.AddAsync(transaction);

            return new TransactionResponse
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Frequency = transaction.Frequency,
                TransactionDate = transaction.TransactionDate,
                CategoryId = transaction.CategoryId
            };
        }

        public async Task<TransactionResponse> GetByIdAsync(
            Guid userId,
            Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction is null)
            {
                throw new KeyNotFoundException("Transação não encontrada.");
            }

            if (transaction.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "A transação não pertence ao usuário.");
            }

            return new TransactionResponse
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Frequency = transaction.Frequency,
                TransactionDate = transaction.TransactionDate,
                CategoryId = transaction.CategoryId
            };
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllAsync(Guid userId)
        {
            var transactions = await _transactionRepository.GetAllByUserAsync(userId);

            return transactions.Select(transaction => new TransactionResponse
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Frequency = transaction.Frequency,
                TransactionDate = transaction.TransactionDate,
                CategoryId = transaction.CategoryId
            });
        }

        public async Task<TransactionResponse> UpdateAsync(
            Guid userId,
            Guid id,
            UpdateTransactionRequest request)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction is null)
            {
                throw new KeyNotFoundException("Transação não encontrada.");
            }

            if (transaction.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "A transação não pertence ao usuário.");
            }

            var category = await _categoryRepository.GetByIdAsync(
                request.CategoryId);

            if (category is null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "A categoria não pertence ao usuário.");
            }

            transaction.Update(
                request.Description,
                request.Amount,
                request.Type,
                request.Frequency,
                request.TransactionDate,
                request.CategoryId);

            await _transactionRepository.UpdateAsync(transaction);

            return new TransactionResponse
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Frequency = transaction.Frequency,
                TransactionDate = transaction.TransactionDate,
                CategoryId = transaction.CategoryId
            };
        }

        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction is null)
            {
                throw new KeyNotFoundException("Transação não encontrada.");
            }

            if (transaction.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "A transação não pertence ao usuário.");
            }

            await _transactionRepository.DeleteAsync(transaction);
        }
    }
}