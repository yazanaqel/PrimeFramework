using Application.Abstractions;
using Domain.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IJwtProvider,JwtProvider>();
        services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IMemberRepository,MemberRepository>();

        return services;
    }
}
