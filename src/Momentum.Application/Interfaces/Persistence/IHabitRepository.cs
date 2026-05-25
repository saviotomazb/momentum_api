using Momentum.Domain.Entities;

namespace Momentum.Application.Interfaces.Persistence;

public interface IHabitRepository
{
    Task<List<Habit>> GetAllByUserIdAsync(Guid userId);

    Task<Habit?> GetByIdAsync(Guid id);

    Task<Habit?> GetByIdWithChecksAsync(Guid id);

    Task CreateAsync(Habit habit);

    Task UpdateAsync(Habit habit);

    Task DeleteAsync(Habit habit);
}