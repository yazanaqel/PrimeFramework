using Application.Features.User.RefreshToken;
using Domain.Entities.Users;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.Authentication.JwtSetup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prime.Identity.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prime.Services.Infrastructure.Services;

internal class UserService(
    UserManager<User> userManager,
    ApplicationDbContext applicationDbContext,
    IJwtProvider jwtProvider,
    IOptions<JwtOptions> options,
    RefreshTokenGenerator refreshTokenGenerator) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly JwtOptions _jwtOptions = options.Value;
    private readonly RefreshTokenGenerator _refreshTokenGenerator = refreshTokenGenerator;
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<bool> IsEmailAvailable(string email,CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null)
            return false;

        return true;
    }

    public async Task<RefreshTokenResponse?> LoginAsync(string email,string password,CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null && await _userManager.CheckPasswordAsync(user,password))
            return await GenerateTokensAsync(user);

        return null;
    }

    public async Task<RefreshTokenResponse?> RegisterAsync(AppUser appUser,CancellationToken ct)
    {
        var strategy = _applicationDbContext.Database.CreateExecutionStrategy();

        User user = new User
        {
            Id = appUser.Id.Value,
            UserName = appUser.Email.Value,
            Email = appUser.Email.Value
        };

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync(ct);

            await _userManager.CreateAsync(user,appUser.Password);

            await _userManager.AddToRoleAsync(user,nameof(Roles.USER));

            await _applicationDbContext.SaveChangesAsync(ct);

            await transaction.CommitAsync(ct);
        });

        return await GenerateTokensAsync(user);
    }



    public async Task<RefreshTokenResponse?> RefreshTokenAsync(string accessToken,string refreshToken,CancellationToken ct)
    {
        var principal = GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);

        if(user == null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return await GenerateTokensAsync(user);
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
    private async Task<RefreshTokenResponse> GenerateTokensAsync(User user)
    {
        var accessToken = await _jwtProvider.GenerateAccessToken(user);
        var refreshToken = _refreshTokenGenerator.Generate();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenDays);

        await _userManager.UpdateAsync(user);

        return new RefreshTokenResponse(accessToken,refreshToken);

    }

    public async Task LogoutAsync(UserId userId,CancellationToken ct)
    {
        string userIdString = userId.Value.ToString();
        var user = await _userManager.FindByIdAsync(userIdString);

        if(user is null)
            return;

        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = null;

        await _userManager.UpdateAsync(user);
    }

}
