using Domain.Constants;
using Domain.Entities.Users;
using Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prime.Identity.Infrastructure.Authentication.JWT;

public class JwtTokenService(IOptions<JwtOptions> options,IPermissionService permissionService) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;
    private readonly IPermissionService _permissionService = permissionService;

    public async Task<string> GenerateAccessToken(UserId userId)
    {

        var accessInfo = await _permissionService.GetUserAccessInfoAsync(userId);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, accessInfo.UserInfo.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, accessInfo.UserInfo.Email.ToString()),
        };


        if(accessInfo is not null) 
        {
            foreach(var role in accessInfo.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role.ToUpper()));
            }

            foreach(var permission in accessInfo.Permissions)
            {
                claims.Add(new Claim(CustomClaims.Permissions,permission.ToUpper()));
            }
        }


        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
            signingCredentials: signingCredentials
        );

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
