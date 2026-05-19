using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Infrastructure.Authentication.JWT;
using Prime.Services.Infrastructure.Services;

namespace Infrastructure.Authentication;

internal static class IdentityRegistration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {

        services.AddScoped<IJwtTokenService,JwtTokenService>();

        services.AddScoped<IRefreshTokenService,RefreshTokenService>();

        services.AddScoped<IPermissionService,PermissionService>();

        services.AddSingleton<IAuthorizationHandler,PermissionAuthorizationHandler>();

        services.AddSingleton<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();

        services.AddScoped<IUserService,UserService>();

        return services;
    }
}
