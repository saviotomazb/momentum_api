using Momentum.Application.Common.Pagination;
using Momentum.Application.DTOs.Habits;
using Momentum.Domain.Entities;

namespace Momentum.Application.Interfaces.Persistence;

public interface IHabitRepository
{
    Task<PagedResult<Habit>> GetAllByUserIdAsync(
        Guid userId,
        HabitFilterRequest filter);

    Task<Habit?> GetByIdAsync(Guid id);

    Task<Habit?> GetByIdWithChecksAsync(Guid id);

    Task CreateAsync(Habit habit);

    Task UpdateAsync(Habit habit);

    Task DeleteAsync(Habit habit);
}
