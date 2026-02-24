using Domain.Constants;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;

internal sealed class RolePermissionConfigration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermissions, SchemaNames.Identity);

        builder.HasKey(rp => new { rp.RoleId, rp.Id });


        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        builder.HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.Id);


        //builder.HasData(Create(Roles.ADMIN,Permissions.READ));
        //builder.HasData(Create(Roles.ADMIN,Permissions.WRITE));
        //builder.HasData(Create(Roles.ADMIN,Permissions.MODIFY));
        //builder.HasData(Create(Roles.ADMIN,Permissions.DELETE));

        //builder.HasData(Create(Roles.USER,Permissions.READ));

        builder.HasData(Create(Guid.Parse("11111111-1111-1111-1111-111111111111"),Roles.ADMIN,Permissions.READ));
        builder.HasData(Create(Guid.Parse("11111111-1111-1111-1111-111111111111"),Roles.ADMIN,Permissions.WRITE));
        builder.HasData(Create(Guid.Parse("11111111-1111-1111-1111-111111111111"),Roles.ADMIN,Permissions.MODIFY));
        builder.HasData(Create(Guid.Parse("11111111-1111-1111-1111-111111111111"),Roles.ADMIN,Permissions.DELETE));

        builder.HasData(Create(Guid.Parse("22222222-2222-2222-2222-222222222222"),Roles.USER,Permissions.READ));

    }

    private static RolePermission Create(Guid RoleId, Roles role,Permissions permission)
    {
        return new RolePermission { RoleId = RoleId, Id = ((int)permission),ClaimType = role.ToString().ToUpper(),ClaimValue = permission.ToString().ToUpper() };
    }
}