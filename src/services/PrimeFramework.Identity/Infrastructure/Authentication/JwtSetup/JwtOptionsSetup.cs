using Domain.Constants;
using Infrastructure.Authentication.JwtSetup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebApi.JwtSetup;
public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = AppSettingsSections.Jwt ;
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}