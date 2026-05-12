using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Momentum.Domain.Entities;

namespace Momentum.Infrastructure.Persistence.Configurations;

public class HabitCheckConfiguration : IEntityTypeConfiguration<HabitCheck>
{
    public void Configure(EntityTypeBuilder<HabitCheck> builder)
    {
        builder.ToTable("habit_checks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompletedAt)
            .IsRequired();

        builder.HasOne(x => x.Habit)
            .WithMany(x => x.HabitChecks)
            .HasForeignKey(x => x.HabitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}