using Application.Repositories;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.Authentication.JwtSetup;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApi.JwtSetup;

namespace Infrastructure.Authentication;

internal static class IdentityRegistration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
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

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer();

        services.ConfigureOptions<JwtOptionsSetup>();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthorization();

        services.AddScoped<IPermissionService,PermissionService>();

        services.AddScoped<IJwtProvider,JwtProvider>();

        services.AddSingleton<IAuthorizationHandler,PermissionAuthorizationHandler>();

        services.AddSingleton<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();

        services.AddScoped<IUserIdentity,UserIdentity>();

        return services;
    }
}
