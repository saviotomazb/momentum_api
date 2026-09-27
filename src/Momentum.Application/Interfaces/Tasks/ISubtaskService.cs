using Momentum.Application.DTOs.Tasks.Subtask;

namespace Momentum.Application.Interfaces.Tasks;

public interface ISubtaskService
{
    Task<SubtaskResponse> CreateAsync(
    Guid userId,
    CreateSubtaskRequest request);

    Task<SubtaskResponse> GetByIdAsync(
        Guid userId,
        Guid id);

    Task<IEnumerable<SubtaskResponse>> GetAllByTaskIdAsync(
        Guid userId,
        Guid taskId);

    Task<SubtaskResponse> UpdateAsync(
        Guid userId,
        Guid id,
        UpdateSubtaskRequest request);

    Task DeleteAsync(
        Guid userId,
        Guid id);

}