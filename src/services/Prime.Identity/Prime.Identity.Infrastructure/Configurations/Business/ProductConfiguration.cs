using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Entities.Products;
using Prime.Identity.Domain.Entities.Stores;

namespace Prime.Identity.Infrastructure.Configurations.Business;


internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products,SchemaNames.Business);
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
.HasConversion(
id => id.Value,
value => new ProductId(value)
);

        builder.Property(c => c.StoreId)
.HasConversion(
id => id.Value,
value => new StoreId(value)
);

        builder.Property(c => c.CategoryId)
.HasConversion(
id => id.Value,
value => new CategoryId(value)
);

        // Store relationship
        builder.HasOne(p => p.Store)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Category (simple one-to-many)
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(p => p.StoreId);
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.Name);

    }

}