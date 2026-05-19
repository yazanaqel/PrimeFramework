using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prime.Identity.Infrastructure.Authentication.JWT;
using System.Text;

namespace Prime.Identity.WebApi.Configuration.JWT;

public class JwtBearerSetup : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public JwtBearerSetup(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void PostConfigure(string? name,JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };

    }
}