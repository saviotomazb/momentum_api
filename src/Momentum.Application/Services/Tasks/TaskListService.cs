using Momentum.Application.DTOs.Tasks.TaskList;
using Momentum.Application.Exceptions;
using Momentum.Application.Interfaces.Persistence.Tasks;
using Momentum.Application.Interfaces.Tasks;
using DomainTaskList = Momentum.Domain.Entities.Tasks.TaskList;

namespace Momentum.Application.Services.Tasks;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly ITaskRepository _taskRepository;

    public TaskListService(
        ITaskListRepository taskListRepository,
        ITaskRepository taskRepository)
    {
        _taskListRepository = taskListRepository;
        _taskRepository = taskRepository;
    }

    public async Task<TaskListResponse> CreateAsync(
        Guid userId,
        CreateTaskListRequest request)
    {
        var taskList = new DomainTaskList(
            request.Name,
            userId);

        await _taskListRepository.AddAsync(taskList);

        return MapToResponse(taskList);
    }

    public async Task<TaskListResponse> GetByIdAsync(
        Guid userId,
        Guid id)
    {
        var taskList = await _taskListRepository
            .GetByIdAsync(id, userId);

        if (taskList is null)
        {
            throw new NotFoundException(
                "Lista de tarefas não encontrada.");
        }

        return MapToResponse(taskList);
    }

    public async Task<IEnumerable<TaskListResponse>> GetAllAsync(
        Guid userId)
    {
        var taskLists = await _taskListRepository
            .GetAllAsync(userId);

        return taskLists.Select(MapToResponse);
    }

    public async Task<TaskListResponse> UpdateAsync(
        Guid userId,
        Guid id,
        UpdateTaskListRequest request)
    {
        var taskList = await _taskListRepository
            .GetByIdAsync(id, userId);

        if (taskList is null)
        {
            throw new NotFoundException(
                "Lista de tarefas não encontrada.");
        }

        taskList.Update(request.Name);

        await _taskListRepository.UpdateAsync(taskList);

        return MapToResponse(taskList);
    }

    public async Task DeleteAsync(
        Guid userId,
        Guid id)
    {
        var taskList = await _taskListRepository
            .GetByIdAsync(id, userId);

        if (taskList is null)
        {
            throw new NotFoundException(
                "Lista de tarefas não encontrada.");
        }

        var hasPendingTasks = await _taskRepository
            .HasPendingTasksAsync(id, userId);

        if (hasPendingTasks)
        {
            throw new ForbiddenException(
                "Não é possível excluir uma lista que possui tarefas pendentes.");
        }

        await _taskListRepository.DeleteAsync(taskList);
    }

    private static TaskListResponse MapToResponse(
        DomainTaskList taskList)
    {
        return new TaskListResponse
        {
            Id = taskList.Id,
            Name = taskList.Name,
            UserId = taskList.UserId
        };
    }

}