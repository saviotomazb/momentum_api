using Microsoft.EntityFrameworkCore;
using Momentum.Domain.Entities;
using Momentum.Domain.Entities.Finance;
using Momentum.Domain.Entities.Tasks;
using Task = Momentum.Domain.Entities.Tasks.Task;

namespace Momentum.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Habit> Habits => Set<Habit>();

    public DbSet<HabitCheck> HabitChecks => Set<HabitCheck>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<TaskList> TaskLists => Set<TaskList>();

    public DbSet<Task> Tasks => Set<Task>();

    public DbSet<Subtask> Subtasks => Set<Subtask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}