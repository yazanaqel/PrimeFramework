using Domain.Constants;
using Domain.Entities.Users;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions, SchemaNames.Identity);

        builder.HasKey(x => x.Id);

        IEnumerable<Permission> permissions = Enum.GetValues<Permissions>()
            .Select
            (permission =>

                new Permission { Id = (int)permission, PermissionName = permission.ToString().ToUpper(), NormalizedName = permission.ToString().ToUpper() }
            );

        builder.HasData(permissions);
    }

}

