using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;
internal sealed class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {

        builder.ToTable(TableNames.UserTokens, SchemaNames.Identity);
        builder.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });


    }

}