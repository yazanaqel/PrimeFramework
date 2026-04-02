using Domain.Constants;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;
internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {

        builder.ToTable(TableNames.UserRoles, SchemaNames.Identity);

        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

    }

}
