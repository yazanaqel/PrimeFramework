using Application.Abstractions;
using Domain.Primitives;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User,Role,Guid,IdentityUserClaim<Guid>,UserRole,IdentityUserLogin<Guid>,RolePermission,IdentityUserToken<Guid>>, IAppDbContext, IUnitOfWork

{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder.ApplyConfigurationsFromAssembly(AssemblyProvider.GetAssembly());


    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {

        // 1. Save changes
        var result = await base.SaveChangesAsync(ct);

        // 2. Extract domain events from tracked entities
        var domainEvents = ChangeTracker
            .Entries<Entity<Guid>>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        // 3. Clear events from entities
        foreach(var entry in ChangeTracker.Entries<Entity<Guid>>())
            entry.Entity.ClearDomainEvents();

        // 4. Dispatch events
        await _domainEventDispatcher.DispatchAsync(domainEvents);

        return result;
    }


}
