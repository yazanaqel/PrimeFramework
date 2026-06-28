using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Prime.Identity.Queries.WebApi.Configuration.Jwt;

public static class IdentityRegistration
{
    public static IServiceCollection AddLocalIdentity(this IServiceCollection services)
    {
        //services.AddIdentityCore<User>(op =>
        //{
        //    op.Password.RequireDigit = false;
        //    op.Password.RequiredLength = 6;
        //    op.Password.RequireUppercase = false;
        //    op.Password.RequireLowercase = false;
        //    op.Password.RequireNonAlphanumeric = false;
        //    op.SignIn.RequireConfirmedAccount = false;
        //    //op.ClaimsIdentity.UserIdClaimType = "UserId";
        //})
        //.AddRoles<Role>()
        //.AddRoleManager<RoleManager<Role>>()
        //.AddSignInManager<SignInManager<User>>()
        //.AddEntityFrameworkStores<ApplicationDbContext>()
        //.AddDefaultTokenProviders()
        //.AddApiEndpoints();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer();

        services.AddAuthorization();

        services.ConfigureOptions<JwtConfiguration>();

        services.ConfigureOptions<JwtBearerSetup>();

        return services;
    }

}