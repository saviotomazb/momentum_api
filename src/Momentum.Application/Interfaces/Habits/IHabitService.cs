using Momentum.Application.DTOs.Habits;

namespace Momentum.Application.Interfaces.Habits;

public interface IHabitService
{
    Task<List<HabitResponse>> GetAllAsync(Guid userId);

    Task<HabitResponse?> GetByIdAsync(Guid id, Guid userId);

    Task<HabitResponse> CreateAsync(
        Guid userId,
        CreateHabitRequest request);

    Task<HabitResponse?> UpdateAsync(
        Guid id,
        Guid userId,
        UpdateHabitRequest request);

    Task<bool> DeleteAsync(Guid id, Guid userId);

    Task<bool> CompleteAsync(Guid id, Guid userId);
}