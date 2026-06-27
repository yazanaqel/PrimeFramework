using Domain.Constants;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Entities.Stores;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Infrastructure.Configurations.Business;


internal sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable(TableNames.Stores,SchemaNames.Business);
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
.HasConversion(
id => id.Value,
value => new StoreId(value)
);

        builder.Property(c => c.UserId)
.HasConversion(
id => id.Value,
value => new UserId(value));

        builder.Property(c => c.CategoryId)
.HasConversion(
id => id.Value,
value => new CategoryId(value)
);

        // Owner
        builder.Property(s => s.UserId)
            .IsRequired();


        // Category (simple one-to-many)
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);



        // Indexes
        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => s.CategoryId);

    }

}