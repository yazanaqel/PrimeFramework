using Application.Abstractions;
using Application.Notifications.Email;
using Application.Repositories;
using Domain.Constants;
using Infrastructure.Authentication;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.DatabaseSeed;
using Infrastructure.Identity;
using Infrastructure.Notifications.Email;
using Infrastructure.Notifications.EventDispatcher;
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

        services.AddIdentity<User,Role>(op =>
        {
            op.Password.RequireDigit = false;
            op.Password.RequiredLength = 6;
            op.Password.RequireUppercase = false;
            op.Password.RequireLowercase = false;
            op.Password.RequireNonAlphanumeric = false;
            op.SignIn.RequireConfirmedAccount = false;
            //op.ClaimsIdentity.UserIdClaimType = "UserId";
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        //services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IPermissionService,PermissionService>();

        services.AddScoped<IJwtProvider,JwtProvider>();

        services.AddSingleton<IAuthorizationHandler,PermissionAuthorizationHandler>();

        services.AddSingleton<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();

        services.AddEmail(configuration);

        services.AddScoped<IDomainEventDispatcher,DomainEventDispatcher>();

        services.AddScoped<IUnitOfWork,ApplicationDbContext>();

        services.AddScoped<IUserIdentity,UserIdentity>();

        services.AddScoped<ISeeder,Seeder>();

        return services;
    }
}
