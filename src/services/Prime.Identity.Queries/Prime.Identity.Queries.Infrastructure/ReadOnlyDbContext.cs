using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Infrastructure;

public class ReadOnlyDbContext : DbContext
{
    public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable("Users","Identity");
        });

        builder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories","Business");
        });

        builder.Entity<Store>(entity =>
        {
            entity.ToTable("Stores","Business");
        });
    }

    public override int SaveChanges()
        => throw new InvalidOperationException("This context is read-only.");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => throw new InvalidOperationException("This context is read-only.");
}


