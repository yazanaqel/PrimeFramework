using Application.Abstractions;
using Domain.Constants;
using Infrastructure.Authentication;
using Infrastructure.DatabaseSeed;
using Infrastructure.Jobs;
using Infrastructure.Notifications.Email;
using Infrastructure.Notifications.EventDispatcher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Domain.Repositories;
using Prime.Identity.Infrastructure.Abstractions;
using Prime.Identity.Infrastructure.Services;

namespace Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(AppSettingsSections.DefaultConnection));
            options.EnableDetailedErrors();
        });

        services.AddEmail(configuration);

        services.AddJobs(configuration);

        services.AddIdentity();

        services.AddSeeder();

        services.AddScoped<IDomainEventDispatcher,DomainEventDispatcher>();

        services.AddScoped<IUnitOfWork,ApplicationDbContext>();

        services.AddScoped<IImageService,ImageService>();

        services.AddScoped<IOrderItemService,OrderItemService>();

        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

        return services;
    }
}
