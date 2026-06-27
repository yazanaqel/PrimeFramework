using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Entities.Products;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Infrastructure.Configurations.Business;


internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Orders,SchemaNames.Business);

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
            .HasConversion(
        id => id.Value,
        value => new OrderId(value)
    );

        builder.Property(c => c.UserId)
    .HasConversion(
id => id.Value,
value => new UserId(value));

        builder.Property(c => c.ProductId)
.HasConversion(
id => id.Value,
value => new ProductId(value)
);

        // Store relationship
        builder.HasOne(o => o.Store)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StoreId)
            .OnDelete(DeleteBehavior.NoAction);


        // Indexes
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.HasIndex(o => o.StoreId);
        builder.HasIndex(o => o.UserId);
    }

}