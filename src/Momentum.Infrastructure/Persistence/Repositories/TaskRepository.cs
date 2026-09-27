using Microsoft.EntityFrameworkCore;
using Momentum.Application.Interfaces.Persistence.Tasks;
using Momentum.Infrastructure.Persistence.Context;
using DomainTask = Momentum.Domain.Entities.Tasks.Task;
using TaskStatus = Momentum.Domain.Enums.Tasks.TaskStatus;
using Task = System.Threading.Tasks.Task;

namespace Momentum.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DomainTask?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }

    public async Task<IEnumerable<DomainTask>> GetAllAsync(Guid userId)
    {
        return await _context.Tasks
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<DomainTask>> GetByTaskListIdAsync(
        Guid taskListId,
        Guid userId)
    {
        return await _context.Tasks
            .Where(x => x.TaskListId == taskListId && x.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> HasPendingTasksAsync(
        Guid taskListId,
        Guid userId)
    {
        return await _context.Tasks
            .AnyAsync(x =>
                x.TaskListId == taskListId &&
                x.UserId == userId &&
                x.Status != TaskStatus.Completed);
    }

    public async Task AddAsync(DomainTask task)
    {
        await _context.Tasks.AddAsync(task);
    }

    public Task UpdateAsync(DomainTask task)
    {
        _context.Tasks.Update(task);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(DomainTask task)
    {
        _context.Tasks.Remove(task);

        return Task.CompletedTask;
    }

}