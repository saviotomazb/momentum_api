using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Momentum.Domain.Entities.Tasks;

namespace Momentum.Infrastructure.Persistence.Configurations.Tasks
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.ToTable("task_lists");

                    builder.HasKey(x => x.Id);

                    builder.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                    builder.HasOne(x => x.User)
                        .WithMany()
                        .HasForeignKey(x => x.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

                    builder.HasIndex(x => x.UserId);
        }
    }
}