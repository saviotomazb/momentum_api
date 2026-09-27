using Momentum.Domain.Entities.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Momentum.Application.Interfaces.Persistence.Tasks;

public interface ITaskRepository
{
    Task<Task?> GetByIdAsync(Guid id, Guid userId);

    Task<IEnumerable<Task>> GetAllAsync(Guid userId);

    Task<IEnumerable<Task>> GetByTaskListIdAsync(Guid taskListId, Guid userId);

    Task<bool> HasPendingTasksAsync(Guid taskListId, Guid userId);

    Task AddAsync(Task task);

    Task UpdateAsync(Task task);

    Task DeleteAsync(Task task);

}