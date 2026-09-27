using Microsoft.EntityFrameworkCore;
using Momentum.Application.Interfaces.Persistence.Tasks;
using Momentum.Domain.Entities.Tasks;
using Momentum.Infrastructure.Persistence.Context;
using Task = System.Threading.Tasks.Task;

namespace Momentum.Infrastructure.Persistence.Repositories;

public class SubtaskRepository : ISubtaskRepository
{
    private readonly AppDbContext _context;

    public SubtaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Subtask?> GetByIdAsync(Guid id)
    {
        return await _context.Subtasks
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Subtask>> GetAllByTaskIdAsync(Guid taskId)
    {
        return await _context.Subtasks
            .Where(x => x.TaskId == taskId)
            .ToListAsync();
    }

    public async Task AddAsync(Subtask subtask)
    {
        await _context.Subtasks.AddAsync(subtask);
    }

    public Task UpdateAsync(Subtask subtask)
    {
        _context.Subtasks.Update(subtask);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Subtask subtask)
    {
        _context.Subtasks.Remove(subtask);

        return Task.CompletedTask;
    }

}