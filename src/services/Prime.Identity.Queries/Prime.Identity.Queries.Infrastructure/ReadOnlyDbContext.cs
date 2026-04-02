using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ReadOnlyDbContext : DbContext
{
    public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable("Users","Identity");
        });
    }

    public override int SaveChanges()
        => throw new InvalidOperationException("This context is read-only.");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => throw new InvalidOperationException("This context is read-only.");
}


