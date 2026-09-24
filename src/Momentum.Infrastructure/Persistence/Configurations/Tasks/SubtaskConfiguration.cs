using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Momentum.Domain.Entities.Tasks;

namespace Momentum.Infrastructure.Persistence.Configurations.Tasks
{
    public class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
    {
        public void Configure(EntityTypeBuilder<Subtask> builder)
        {
            builder.ToTable("subtasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            builder.Property(x => x.ScheduledDate);

            builder.Property(x => x.DueDate);

            builder.HasOne(x => x.Task)
                .WithMany(x => x.Subtasks)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.TaskId);

            builder.HasIndex(x => x.DueDate);
        }
    }
}