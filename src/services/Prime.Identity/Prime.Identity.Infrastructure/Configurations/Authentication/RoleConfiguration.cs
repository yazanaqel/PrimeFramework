using Domain.Constants;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {

        builder.ToTable(TableNames.Roles,SchemaNames.Identity);

    }
}

