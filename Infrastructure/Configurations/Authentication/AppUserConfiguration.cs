using Domain.Constants;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Authentication;

internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUser",SchemaNames.Identity);

        builder.HasKey(x => x.Id);

        builder.Property(u => u.Email).HasConversion(email => email.Value,
            value => Email.Create(value)).HasMaxLength(255);
    }

}