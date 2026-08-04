using Momentum.Domain.Common;

namespace Momentum.Domain.Entities.Finance
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public string Color { get; private set; } = string.Empty;

        public string Icon { get; private set; } = string.Empty;

        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;

        public ICollection<Transaction> Transactions { get; private set; } = [];

        private Category()
        {
        }

        public Category(string name, string color, string icon, Guid userId)
        {
            Name = name;
            Color = color;
            Icon = icon;
            UserId = userId;
        }

        public void Update(string name, string color, string icon)
        {
            Name = name;
            Color = color;
            Icon = icon;
            UpdatedAt = DateTime.UtcNow;
        }
    
    }
}