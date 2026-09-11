using Momentum.Application.Interfaces.Categories;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Application.DTOs.Categories;
using Momentum.Application.Exceptions;
using Momentum.Domain.Entities.Finance;

namespace Momentum.Application.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponse> CreateAsync(
        Guid userId,
        CreateCategoryRequest request)
    {
        var existingCategory = await _categoryRepository
            .GetByNameAsync(userId, request.Name);

        if (existingCategory is not null)
        {
            throw new ConflictException(
                $"Categoria com o nome '{request.Name}' já existe.");
        }

        var category = new Category(
            request.Name,
            request.Color,
            request.Icon,
            userId);

        await _categoryRepository.AddAsync(category);

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Icon = category.Icon
        };
    }

    public async Task<CategoryResponse?> GetByIdAsync(
        Guid userId,
        Guid categoryId)
    {
        var category = await _categoryRepository
            .GetByIdAsync(categoryId);

        if (category is null)
        {
            throw new NotFoundException(
                "Categoria não encontrada.");
        }

        if (category.UserId != userId)
        {
            throw new ForbiddenException(
                "Acesso negado à categoria.");
        }

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Icon = category.Icon
        };
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync(
        Guid userId)
    {
        var categories = await _categoryRepository
            .GetAllByUserAsync(userId);

        return categories.Select(category => new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Icon = category.Icon
        });
    }

    public async Task<CategoryResponse> UpdateAsync(
        Guid userId,
        Guid categoryId,
        UpdateCategoryRequest request)
    {
        var category = await _categoryRepository
            .GetByIdAsync(categoryId);

        if (category is null)
        {
            throw new NotFoundException(
                "Categoria não encontrada.");
        }

        if (category.UserId != userId)
        {
            throw new ForbiddenException(
                "Acesso negado à categoria.");
        }

        var existingCategory = await _categoryRepository
            .GetByNameAsync(userId, request.Name);

        if (existingCategory is not null &&
            existingCategory.Id != categoryId)
        {
            throw new ConflictException(
                $"Categoria com o nome '{request.Name}' já existe.");
        }

        category.Update(
            request.Name,
            request.Color,
            request.Icon);

        await _categoryRepository.UpdateAsync(category);

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            Icon = category.Icon
        };
    }

    public async Task DeleteAsync(
        Guid userId,
        Guid categoryId)
    {
        var category = await _categoryRepository
            .GetByIdAsync(categoryId);

        if (category is null)
        {
            throw new NotFoundException(
                "Categoria não encontrada.");
        }

        if (category.UserId != userId)
        {
            throw new ForbiddenException(
                "Acesso negado à categoria.");
        }

        await _categoryRepository.DeleteAsync(category);
    }
}