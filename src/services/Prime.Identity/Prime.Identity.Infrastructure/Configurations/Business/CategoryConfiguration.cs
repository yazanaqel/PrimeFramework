using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Identity.Domain.Entities.Categories;

namespace Prime.Identity.Infrastructure.Configurations.Business;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Categories,SchemaNames.Business);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => new CategoryId(value)
            );

        builder.Property(c => c.ParentCategoryId)
            .HasConversion(
                id => id.HasValue ? id.Value.Value : (Guid?)null,
                value => value.HasValue ? new CategoryId(value.Value) : (CategoryId?)null
            );



        // Self-referencing relationship
        builder
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        // Prevents cascade delete loops

        // Indexes
        builder.HasIndex(c => new { c.Name,c.ParentCategoryId })
       .IsUnique();


    }

}

