using Momentum.Domain.Entities.Finance;

namespace Momentum.Application.Interfaces.Persistence
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);

        Task<Category?> GetByIdAsync(Guid id);

        Task<Category?> GetByNameAsync(Guid userId, string name);

        Task<IEnumerable<Category>> GetAllByUserAsync(Guid userId);

        Task UpdateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}