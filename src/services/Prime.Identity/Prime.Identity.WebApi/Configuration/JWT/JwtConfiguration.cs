using Domain.Constants;
using Microsoft.Extensions.Options;
using Prime.Identity.Infrastructure.Authentication.JWT;

namespace Prime.Identity.WebApi.Configuration.JWT;

public class JwtConfiguration : IConfigureOptions<JwtOptions>
{
    private const string SectionName = AppSettingsSections.Jwt;
    private readonly IConfiguration _configuration;

    public JwtConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
