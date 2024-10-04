using Domain.Constants;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles, SchemaNames.Identity);

        IEnumerable<Role> roles = Enum.GetValues<Roles>()
            .Select
            (role =>

                new Role { Id = (int)role, Name = role.ToString().ToLower(), NormalizedName = role.ToString().ToUpper() }
            );

        builder.HasData(roles);
    }
}