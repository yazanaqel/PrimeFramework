using Domain.Constants;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Infrastructure.Configurations.Authentication;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles, SchemaNames.Identity);

        //IEnumerable<Role> roles = Enum.GetValues<Roles>()
        //    .Select
        //    (role =>

        //        new Role { Id = , Name = role.ToString().ToUpper(), NormalizedName = role.ToString().ToUpper() }
        //    );

        IEnumerable<Role> roles = [
            new Role {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Admin",
                NormalizedName = "ADMIN"},
                    new Role {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "User",
                NormalizedName = "USER"}];


        builder.HasData(roles);
    }
}