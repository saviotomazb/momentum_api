using Momentum.Domain.Entities.Tasks;
using DomainTask = Momentum.Domain.Entities.Tasks.Task;
using Task = System.Threading.Tasks.Task;

namespace Momentum.Application.Interfaces.Persistence.Tasks;

public interface ITaskRepository
{
    Task<DomainTask?> GetByIdAsync(Guid id, Guid userId);

    Task<IEnumerable<DomainTask>> GetAllAsync(Guid userId);

    Task<IEnumerable<DomainTask>> GetByTaskListIdAsync(
        Guid taskListId,
        Guid userId);

    Task<bool> HasPendingTasksAsync(
        Guid taskListId,
        Guid userId);

    Task AddAsync(DomainTask task);

    Task UpdateAsync(DomainTask task);

    Task DeleteAsync(DomainTask task);

}