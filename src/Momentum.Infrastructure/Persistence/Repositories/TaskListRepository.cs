
using Microsoft.EntityFrameworkCore;
using Momentum.Application.Interfaces.Persistence.Tasks;
using Momentum.Domain.Entities.Tasks;
using Momentum.Infrastructure.Persistence.Context;
using Task = System.Threading.Tasks.Task;
using Momentum.Application.Exceptions;
using Npgsql;

namespace Momentum.Infrastructure.Persistence.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly AppDbContext _context;

    public TaskListRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskList?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.TaskLists
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }

    public async Task<IEnumerable<TaskList>> GetAllAsync(Guid userId)
    {
        return await _context.TaskLists
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(TaskList taskList)
    {
        await _context.TaskLists.AddAsync(taskList);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (IsDuplicateTaskListName(ex))
        {
            throw new ConflictException(
                "Você já possui uma lista com esse nome.");
        }
    }

    public async Task UpdateAsync(TaskList taskList)
    {
        _context.TaskLists.Update(taskList);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (IsDuplicateTaskListName(ex))
        {
            throw new ConflictException(
                "Você já possui uma lista com esse nome.");
        }
    }

    private static bool IsDuplicateTaskListName(DbUpdateException exception)
    {
        return exception.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.UniqueViolation,
            ConstraintName: "IX_task_lists_UserId_NormalizedName"
        };
    }

    public async Task DeleteAsync(TaskList taskList)
    {
        _context.TaskLists.Remove(taskList);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(Guid userId, string name, Guid? excludeId = null)
    {
        var normalizedName = name.Trim().ToLower();

        return await _context.TaskLists.AnyAsync(x =>
            x.UserId == userId &&
            x.Name.Trim().ToLower() == normalizedName &&
            (excludeId == null || x.Id != excludeId));
    }
}