using Momentum.Domain.Common;

namespace Momentum.Domain.Entities.Tasks
{
    public class TaskList : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;

        public ICollection<Task> Tasks { get; private set; } = [];

        private TaskList()
        {
        }

        public TaskList(string name, Guid userId)
        {
            Name = name;
            UserId = userId;
        }

        public void Update(string name)
        {
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}