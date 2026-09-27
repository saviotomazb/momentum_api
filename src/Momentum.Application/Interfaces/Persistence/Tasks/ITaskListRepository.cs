using Momentum.Domain.Entities.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Momentum.Application.Interfaces.Persistence.Tasks;

public interface ITaskListRepository
{
    Task<TaskList?> GetByIdAsync(Guid id, Guid userId);

    Task<IEnumerable<TaskList>> GetAllAsync(Guid userId);

    Task AddAsync(TaskList taskList);

    Task UpdateAsync(TaskList taskList);

    Task DeleteAsync(TaskList taskList);

}