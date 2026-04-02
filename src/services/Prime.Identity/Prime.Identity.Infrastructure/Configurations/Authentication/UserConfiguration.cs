using Domain.Constants;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users, SchemaNames.Identity);

    }
}
