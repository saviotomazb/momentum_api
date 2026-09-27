using Momentum.Application.DTOs.Tasks.Subtask;
using Momentum.Application.Exceptions;
using Momentum.Application.Interfaces.Persistence.Tasks;
using Momentum.Application.Interfaces.Tasks;
using DomainSubtask = Momentum.Domain.Entities.Tasks.Subtask;

namespace Momentum.Application.Services.Tasks;

public class SubtaskService : ISubtaskService
{
    private readonly ISubtaskRepository _subtaskRepository;
    private readonly ITaskRepository _taskRepository;

    public SubtaskService(
        ISubtaskRepository subtaskRepository,
        ITaskRepository taskRepository)
    {
        _subtaskRepository = subtaskRepository;
        _taskRepository = taskRepository;
    }

    public async Task<SubtaskResponse> CreateAsync(
        Guid userId,
        CreateSubtaskRequest request)
    {
        var task = await _taskRepository
            .GetByIdAsync(request.TaskId, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Tarefa não encontrada.");
        }

        var subtask = new DomainSubtask(
            request.TaskId,
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            request.ScheduledDate,
            request.DueDate);

        await _subtaskRepository.AddAsync(subtask);

        return MapToResponse(subtask);
    }

    public async Task<SubtaskResponse> GetByIdAsync(
        Guid userId,
        Guid id)
    {
        var subtask = await _subtaskRepository
            .GetByIdAsync(id);

        if (subtask is null)
        {
            throw new NotFoundException(
                "Subtarefa não encontrada.");
        }

        var task = await _taskRepository
            .GetByIdAsync(subtask.TaskId, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Subtarefa não encontrada.");
        }

        return MapToResponse(subtask);
    }

    public async Task<IEnumerable<SubtaskResponse>> GetAllByTaskIdAsync(
        Guid userId,
        Guid taskId)
    {
        var task = await _taskRepository
            .GetByIdAsync(taskId, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Tarefa não encontrada.");
        }

        var subtasks = await _subtaskRepository
            .GetAllByTaskIdAsync(taskId);

        return subtasks.Select(MapToResponse);
    }

    public async Task<SubtaskResponse> UpdateAsync(
        Guid userId,
        Guid id,
        UpdateSubtaskRequest request)
    {
        var subtask = await _subtaskRepository
            .GetByIdAsync(id);

        if (subtask is null)
        {
            throw new NotFoundException(
                "Subtarefa não encontrada.");
        }

        var task = await _taskRepository
            .GetByIdAsync(subtask.TaskId, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Subtarefa não encontrada.");
        }

        subtask.Update(
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            request.ScheduledDate,
            request.DueDate);

        await _subtaskRepository.UpdateAsync(subtask);

        return MapToResponse(subtask);
    }

    public async Task DeleteAsync(
        Guid userId,
        Guid id)
    {
        var subtask = await _subtaskRepository
            .GetByIdAsync(id);

        if (subtask is null)
        {
            throw new NotFoundException(
                "Subtarefa não encontrada.");
        }

        var task = await _taskRepository
            .GetByIdAsync(subtask.TaskId, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Subtarefa não encontrada.");
        }

        await _subtaskRepository.DeleteAsync(subtask);
    }

    private static SubtaskResponse MapToResponse(
        DomainSubtask subtask)
    {
        return new SubtaskResponse
        {
            Id = subtask.Id,
            TaskId = subtask.TaskId,
            Title = subtask.Title,
            Description = subtask.Description,
            Status = subtask.Status,
            Priority = subtask.Priority,
            ScheduledDate = subtask.ScheduledDate,
            DueDate = subtask.DueDate
        };
    }

}