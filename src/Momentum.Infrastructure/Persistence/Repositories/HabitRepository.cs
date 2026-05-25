using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Habit>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Habits
            .Where(x => x.UserId == userId)
            .Include(x => x.HabitChecks)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
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