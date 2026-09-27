using Momentum.Domain.Entities.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Momentum.Application.Interfaces.Persistence.Tasks;

public interface ISubtaskRepository
{
    Task<Subtask?> GetByIdAsync(Guid id);

    Task<IEnumerable<Subtask>> GetAllByTaskIdAsync(Guid taskId);

    Task AddAsync(Subtask subtask);

    Task UpdateAsync(Subtask subtask);

    Task DeleteAsync(Subtask subtask);

}