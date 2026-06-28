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
using System.Security.Cryptography;
using System.Text;

namespace Prime.Identity.Infrastructure.Authentication.JWT;

public class JwtTokenService(
    IOptions<JwtOptions> options,
     UserManager<User> userManager,
     IOptions<JwtOptions> jwtOptions,
    IPermissionService permissionService) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;
    private readonly UserManager<User> _userManager = userManager;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly IPermissionService _permissionService = permissionService;

    public async Task<TokenResponse> GenerateAccessTokenAsync(UserId userId,CancellationToken ct)
    {

        var accessInfo = await _permissionService.GetUserAccessInfoAsync(userId);

        if(accessInfo is null)
        {
            return new TokenResponse(string.Empty,string.Empty,DateTime.UtcNow,DateTime.UtcNow);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, accessInfo.User.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, accessInfo.User.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, accessInfo.User.Email!),

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


        var refreshToken = GenerateRefreshToken();

        accessInfo.User.RefreshToken = refreshToken;
        accessInfo.User.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_options.RefreshTokenDays);

        await _userManager.UpdateAsync(accessInfo.User);

        return new TokenResponse(accessToken,refreshToken,
            DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
            DateTime.UtcNow.AddDays(_options.RefreshTokenDays));
    }


    public async Task<TokenResponse> RefreshTokenAsync(string accessToken,string refreshToken,CancellationToken ct)
    {
        var principal = GetPrincipalFromExpiredToken(accessToken);

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if(userId is null)
            return new TokenResponse(string.Empty,string.Empty,DateTime.UtcNow,DateTime.UtcNow);

        var user = await _userManager.FindByIdAsync(userId);

        if(user == null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new TokenResponse(string.Empty,string.Empty,DateTime.UtcNow,DateTime.UtcNow);
        }

        UserId.TryParse(user.Id.ToString(),out UserId parsedUserId);

        return await GenerateAccessTokenAsync(parsedUserId, ct);

    }

    private string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken,tokenValidationParameters,out _);
        return principal;
    }
}
