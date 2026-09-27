using Momentum.Application.DTOs.Tasks.TaskList;

namespace Momentum.Application.Interfaces.Tasks;

public interface ITaskListService
{
    Task<TaskListResponse> CreateAsync(
    Guid userId,
    CreateTaskListRequest request);

    Task<TaskListResponse> GetByIdAsync(
        Guid userId,
        Guid id);

    Task<IEnumerable<TaskListResponse>> GetAllAsync(
        Guid userId);

    Task<TaskListResponse> UpdateAsync(
        Guid userId,
        Guid id,
        UpdateTaskListRequest request);

    Task DeleteAsync(
        Guid userId,
        Guid id);

}