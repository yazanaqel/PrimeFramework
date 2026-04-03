using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Identity.Infrastructure.Authentication.IdentityEntities;

namespace Infrastructure.Configurations.Authentication;

internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable(TableNames.UserPermissions,SchemaNames.Identity);

        builder.HasKey(ur => new { ur.Id });

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(ur => ur.UserId);

    }
}