using Momentum.Application.Common.Pagination;
using Momentum.Domain.Enums;

namespace Momentum.Application.DTOs.Habits;

public class HabitFilterRequest : PaginationRequest
{
    public string? Search { get; set; }

    public HabitFrequency? Frequency { get; set; }

    public bool? CompletedToday { get; set; }
}
