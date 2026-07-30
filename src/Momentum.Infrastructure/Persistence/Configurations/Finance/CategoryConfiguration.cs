using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Momentum.Domain.Entities.Finance;

namespace Momentum.Infrastructure.Persistence.Configurations.Finance
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Color)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Icon)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new
            {
                c.UserId,
                c.Name
            }).IsUnique();
                
            builder.Property(t => t.CreatedAt)
                .IsRequired();
        }
    }
}