using Momentum.Application.DTOs.Tasks.Task;
using Momentum.Application.Exceptions;
using Momentum.Application.Interfaces.Persistence.Tasks;
using Momentum.Application.Interfaces.Tasks;
using DomainTask = Momentum.Domain.Entities.Tasks.Task;

namespace Momentum.Application.Services.Tasks;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskListRepository _taskListRepository;

    public TaskService(
        ITaskRepository taskRepository,
        ITaskListRepository taskListRepository)
    {
        _taskRepository = taskRepository;
        _taskListRepository = taskListRepository;
    }

    public async Task<TaskResponse> CreateAsync(
        Guid userId,
        CreateTaskRequest request)
    {
        var taskList = await _taskListRepository
            .GetByIdAsync(request.TaskListId, userId);

        if (taskList is null)
        {
            throw new NotFoundException(
                "Lista de tarefas não encontrada.");
        }

        var task = new DomainTask(
            userId,
            request.TaskListId,
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            request.ScheduledDate,
            request.DueDate);

        await _taskRepository.AddAsync(task);

        return MapToResponse(task);
    }

    public async Task<TaskResponse> GetByIdAsync(
        Guid userId,
        Guid id)
    {
        var task = await _taskRepository
            .GetByIdAsync(id, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Tarefa não encontrada.");
        }

        return MapToResponse(task);
    }

    public async Task<IEnumerable<TaskResponse>> GetAllAsync(
        Guid userId)
    {
        var tasks = await _taskRepository
            .GetAllAsync(userId);

        return tasks.Select(MapToResponse);
    }

    public async Task<IEnumerable<TaskResponse>> GetByTaskListIdAsync(
        Guid userId,
        Guid taskListId)
    {
        var taskList = await _taskListRepository
            .GetByIdAsync(taskListId, userId);

        if (taskList is null)
        {
            throw new NotFoundException(
                "Lista de tarefas não encontrada.");
        }

        var tasks = await _taskRepository
            .GetByTaskListIdAsync(taskListId, userId);

        return tasks.Select(MapToResponse);
    }

    public async Task<TaskResponse> UpdateAsync(
        Guid userId,
        Guid id,
        UpdateTaskRequest request)
    {
        var task = await _taskRepository
            .GetByIdAsync(id, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Tarefa não encontrada.");
        }

        task.Update(
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            request.ScheduledDate,
            request.DueDate);

        await _taskRepository.UpdateAsync(task);

        return MapToResponse(task);
    }

    public async Task DeleteAsync(
        Guid userId,
        Guid id)
    {
        var task = await _taskRepository
            .GetByIdAsync(id, userId);

        if (task is null)
        {
            throw new NotFoundException(
                "Tarefa não encontrada.");
        }

        await _taskRepository.DeleteAsync(task);
    }

    private static TaskResponse MapToResponse(DomainTask task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            UserId = task.UserId,
            TaskListId = task.TaskListId,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            ScheduledDate = task.ScheduledDate,
            DueDate = task.DueDate
        };
    }

}