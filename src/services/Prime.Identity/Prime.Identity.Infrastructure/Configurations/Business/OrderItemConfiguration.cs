using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Entities.Products;

namespace Prime.Identity.Infrastructure.Configurations.Business;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(TableNames.OrderItems,SchemaNames.Business);

        builder.HasKey(oi => oi.Id);

        builder.Property(c => c.Id)
    .HasConversion(
        id => id.Value,
        value => new OrderItemId(value)
    );
        builder.Property(oi => oi.ProductId)
    .HasConversion(
        id => id.Value,
        value => new ProductId(value)
    );

        builder.Property(oi => oi.OrderId)
.HasConversion(
id => id.Value,
value => new OrderId(value)
);

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(oi => oi.OrderId);
        builder.HasIndex(oi => oi.ProductId);
    }
}