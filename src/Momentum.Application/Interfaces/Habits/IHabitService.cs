using Momentum.Application.DTOs.Habits;
using Momentum.Application.Common.Pagination;

namespace Momentum.Application.Interfaces.Habits;

public interface IHabitService
{
    Task<PagedResult<HabitResponse>> GetAllAsync(
        Guid userId,
        HabitFilterRequest filter);

    Task<HabitResponse?> GetByIdAsync(Guid id, Guid userId);

    Task<HabitResponse> CreateAsync(
        Guid userId,
        CreateHabitRequest request);

    Task<HabitResponse?> UpdateAsync(
        Guid id,
        Guid userId,
        UpdateHabitRequest request);

    Task<bool> DeleteAsync(Guid id, Guid userId);

    Task<HabitResponse?> CompleteAsync(Guid id, Guid userId);
}
