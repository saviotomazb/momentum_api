using Momentum.Domain.Enums.Tasks;
using TaskStatus = Momentum.Domain.Enums.Tasks.TaskStatus;

namespace Momentum.Application.DTOs.Tasks.Subtask;

public class SubtaskResponse
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskStatus Status { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
