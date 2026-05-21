using Application.Features.User.RefreshToken;
using Domain.Constants;
using Infrastructure.Authentication;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prime.Identity.Infrastructure.Authentication.JWT;

public class JwtTokenService(
    IOptions<JwtOptions> options,
     UserManager<User> userManager,
    IPermissionService permissionService,
    IRefreshTokenService refreshTokenService) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    public async Task<TokenResponse> GenerateAccessToken(UserId userId)
    {

        var accessInfo = await _permissionService.GetUserAccessInfoAsync(userId);

        if(accessInfo is null)
        {
            return new TokenResponse(string.Empty,string.Empty,DateTime.UtcNow,DateTime.UtcNow);
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, accessInfo.User.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, accessInfo.User.Email),
        };

        foreach(var role in accessInfo.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role.ToUpper()));
        }

        foreach(var permission in accessInfo.Permissions)
        {
            claims.Add(new Claim(CustomClaims.Permissions,permission.ToUpper()));
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

        string accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);


        var refreshToken = _refreshTokenService.Generate();

        accessInfo.User.RefreshToken = refreshToken;
        accessInfo.User.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_options.RefreshTokenDays);

        await _userManager.UpdateAsync(accessInfo.User);

        return new TokenResponse(accessToken,refreshToken,
            DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
            DateTime.UtcNow.AddDays(_options.RefreshTokenDays));
    }
}
