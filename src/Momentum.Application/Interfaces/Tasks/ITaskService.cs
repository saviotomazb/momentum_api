using Momentum.Application.DTOs.Tasks.Task;

namespace Momentum.Application.Interfaces.Tasks;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(
    Guid userId,
    CreateTaskRequest request);

    Task<TaskResponse> GetByIdAsync(
        Guid userId,
        Guid id);

    Task<IEnumerable<TaskResponse>> GetAllAsync(
        Guid userId);

    Task<IEnumerable<TaskResponse>> GetByTaskListIdAsync(
        Guid userId,
        Guid taskListId);

    Task<TaskResponse> UpdateAsync(
        Guid userId,
        Guid id,
        UpdateTaskRequest request);

    Task DeleteAsync(
        Guid userId,
        Guid id);

}