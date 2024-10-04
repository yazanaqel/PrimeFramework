using Domain.Constants;
using Domain.Entities.Clients;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(TableNames.Clients);

        builder.HasKey(x => x.ClientId);


        builder
                .HasDiscriminator<string>("ClientType")
                .HasValue<Person>(nameof(Person))
                .HasValue<Company>(nameof(Company));

        builder
            .HasOne(c => (User)c.AuthenticationMember)
            .WithOne()
            .HasForeignKey<Client>(c => c.AuthenticationMemberId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}






