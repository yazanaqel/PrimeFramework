using Application.Abstractions;
using Domain.Constants;
using Infrastructure.Authentication;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.DatabaseSeed;
using Infrastructure.Repositories.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(AppSettingsSections.DefaultConnection));
        });

        services.AddIdentity<User, Role>(op =>
        {
            op.Password.RequireDigit = false;
            op.Password.RequiredLength = 6;
            op.Password.RequireUppercase = false;
            op.Password.RequireLowercase = false;
            op.Password.RequireNonAlphanumeric = false;
            op.SignIn.RequireConfirmedAccount = false;
            //op.ClaimsIdentity.UserIdClaimType = "UserId";
        })
    .AddEntityFrameworkStores<ApplicationDbContext>().
    AddDefaultTokenProviders();

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<ISeeder, Seeder>();

        return services;
    }
}
