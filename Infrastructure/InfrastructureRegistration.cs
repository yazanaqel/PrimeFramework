using Application.Abstractions;
using Application.Behaviors;
using Application.Notifications.Email;
using Application.Repositories;
using Domain.Constants;
using Infrastructure.Authentication;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.DatabaseSeed;
using Infrastructure.Identity;
using Infrastructure.Notifications.Email;
using Infrastructure.Notifications.EventDispatcher;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(AppSettingsSections.DefaultConnection));
            options.EnableDetailedErrors();
            //options.EnableSensitiveDataLogging(); // Only in development
        });

        //services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ApplicationDbContext>());

        services.AddEmail(configuration);

        services.AddIdentity();

        services.AddSeeder();

        services.AddScoped<IDomainEventDispatcher,DomainEventDispatcher>();

        services.AddScoped<IUnitOfWork,ApplicationDbContext>();

        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

        return services;
    }
}
