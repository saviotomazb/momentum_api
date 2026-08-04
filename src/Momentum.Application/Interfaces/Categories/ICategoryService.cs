using Momentum.Application.DTOs.Categories;

namespace Momentum.Application.Interfaces.Categories
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateAsync(Guid userId, CreateCategoryRequest request);

        Task<IEnumerable<CategoryResponse>> GetAllAsync(Guid userId);

        Task<CategoryResponse?> GetByIdAsync(Guid userId, Guid categoryId);

        Task<CategoryResponse> UpdateAsync(Guid userId, Guid categoryId, UpdateCategoryRequest request);

        Task DeleteAsync(Guid userId, Guid categoryId);
    }
}