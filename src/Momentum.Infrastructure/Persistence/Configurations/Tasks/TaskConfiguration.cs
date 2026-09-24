using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Momentum.Domain.Entities.Tasks.Task;

namespace Momentum.Infrastructure.Persistence.Configurations.Tasks
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("tasks");

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

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.TaskList)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.TaskListId);

            builder.HasIndex(x => x.DueDate);
        }
    }
}