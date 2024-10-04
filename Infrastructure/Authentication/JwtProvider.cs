using Domain.Constants;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;
internal class JwtProvider(IOptions<JwtOptions> options, IPermissionService permissionService) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;
    private readonly IPermissionService _permissionService = permissionService;

    public async Task<string> GenerateAsync(User member)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, member.Email.ToString()),
        };

        var rolePermissions = await _permissionService
            .GetRolePermissionsAsync(member.Id);

        foreach (var role in rolePermissions)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Key));

            foreach (var permission in role.Value)
            {
                claims.Add(new Claim(CustomClaims.Permissions, permission));
            }
        }


        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
