using Microsoft.EntityFrameworkCore;
using Momentum.Application.Common.Pagination;
using Momentum.Application.DTOs.Habits;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Domain.Entities;
using Momentum.Infrastructure.Persistence.Context;

namespace Momentum.Infrastructure.Persistence.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly AppDbContext _context;

    public HabitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Habit>> GetAllByUserIdAsync(
        Guid userId,
        HabitFilterRequest filter)
    {
        var today = DateTime.UtcNow.Date;

        var query = _context.Habits
            .Where(x => x.UserId == userId)
            .Include(x => x.HabitChecks)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLower();

            query = query.Where(x =>
                x.Title.ToLower().Contains(search) ||
                (x.Description != null &&
                    x.Description.ToLower().Contains(search)));
        }

        if (filter.Frequency is not null)
        {
            query = query.Where(x => x.Frequency == filter.Frequency);
        }

        if (filter.CompletedToday is not null)
        {
            query = filter.CompletedToday.Value
                ? query.Where(x => x.HabitChecks.Any(check =>
                    check.CompletedAt.Date == today))
                : query.Where(x => !x.HabitChecks.Any(check =>
                    check.CompletedAt.Date == today));
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)filter.PageSize);

        var habits = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<Habit>
        {
            Items = habits,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<Habit?> GetByIdAsync(Guid id)
    {
        return await _context.Habits
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Habit?> GetByIdWithChecksAsync(Guid id)
    {
        return await _context.Habits
            .Include(x => x.HabitChecks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(Habit habit)
    {
        await _context.Habits.AddAsync(habit);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Habit habit)
    {
        _context.Habits.Update(habit);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Habit habit)
    {
        _context.Habits.Remove(habit);

        await _context.SaveChangesAsync();
    }
}
