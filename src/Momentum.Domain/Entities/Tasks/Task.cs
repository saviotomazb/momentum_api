using Momentum.Domain.Common;
using Momentum.Domain.Enums.Tasks;
using TaskStatus = Momentum.Domain.Enums.Tasks.TaskStatus;

namespace Momentum.Domain.Entities.Tasks
{
    public class Task : BaseEntity
    {
        public Guid UserId { get; private set; }

        public Guid TaskListId { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public TaskStatus Status { get; private set; }

        public TaskPriority Priority { get; private set; }

        public DateTime? ScheduledDate { get; private set; }

        public DateTime? DueDate { get; private set; }

        public User User { get; private set; } = null!;

        public TaskList TaskList { get; private set; } = null!;

        public ICollection<Subtask> Subtasks { get; private set; } = [];

        private Task()
        {
        }

        public Task(
            Guid userId,
            Guid taskListId,
            string title,
            string? description,
            TaskStatus status,
            TaskPriority priority,
            DateTime? scheduledDate,
            DateTime? dueDate)
        {
            UserId = userId;
            TaskListId = taskListId;
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            ScheduledDate = scheduledDate;
            DueDate = dueDate;
        }

        public void Update(
            string title,
            string? description,
            TaskStatus status,
            TaskPriority priority,
            DateTime? scheduledDate,
            DateTime? dueDate)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            ScheduledDate = scheduledDate;
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}