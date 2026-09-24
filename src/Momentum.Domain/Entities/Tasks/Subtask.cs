using Momentum.Domain.Common;
using Momentum.Domain.Enums.Tasks;
using TaskStatus = Momentum.Domain.Enums.Tasks.TaskStatus;

namespace Momentum.Domain.Entities.Tasks
{
    public class Subtask : BaseEntity
    {
        public Guid TaskId { get; private set; }
        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public TaskStatus Status { get; private set; }

        public TaskPriority Priority { get; private set; }

        public DateTime? ScheduledDate { get; private set; }

        public DateTime? DueDate { get; private set; }

        public Task Task { get; private set; } = null!;

        private Subtask()
        {
        }

        public Subtask(
            Guid taskId,
            string title,
            string description,
            TaskStatus status,
            TaskPriority priority,
            DateTime? scheduledDate,
            DateTime? dueDate)
        {
            TaskId = taskId;
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            ScheduledDate = scheduledDate;
            DueDate = dueDate;
        }

        public void Update(
            string title,
            string description,
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