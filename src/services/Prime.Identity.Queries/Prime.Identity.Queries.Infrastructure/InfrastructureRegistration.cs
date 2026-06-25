using Domain.Abstractions;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prime.Identity.Queries.Application.Abstractions.Cache;
using Prime.Identity.Queries.Infrastructure.Abstractions;

namespace Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ReadOnlyDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
            options.InstanceName = configuration["Redis:InstanceName"];
        });

        services.AddScoped<ICacheService,CacheService>();

        services.AddScoped(typeof(IReadRepository<>),typeof(ReadRepository<>));

        return services;
    }
}
